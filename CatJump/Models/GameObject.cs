using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public abstract class GameObject
    {
        public bool Visible { get; set; } = true;
        public bool UseGravity { get; set; }
        public bool UseCollisions { get; set; }
        public Texture2D CurrentSprite { get { return graphic.GetCurrentSprite(); } }
        public BoundingBox BoundingBox { get { return GetBoundingBox(); } }
        private BoundingBox _boundingBox;
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 SpritePosition { get { return Position - Origin; } }
        public Vector2 Origin { get; private set; }

        public delegate void Collided(Collision collision);
        public event Collided OnCollision;

        protected World world;
        private Graphic graphic;
        private Rectangle previousPosition;

        public GameObject() { }

        protected void Init(Graphic graphic, Vector2 position)
        {
            this.graphic = graphic;
            Position = position;
            _boundingBox = BoundingBox.FromGraphic(graphic);
            previousPosition = BoundingBox.Rectangle;
            Origin = new Vector2(_boundingBox.Rectangle.Width / 2 + _boundingBox.Offset.X, _boundingBox.Rectangle.Height / 2 + _boundingBox.Offset.Y);
        }

        private BoundingBox GetBoundingBox()
        {
            Point newPosition = new Point((int)SpritePosition.X + _boundingBox.Offset.X, (int)SpritePosition.Y + _boundingBox.Offset.Y);
            _boundingBox.Rectangle = new Rectangle(newPosition, _boundingBox.Rectangle.Size);
            return _boundingBox;
        }

        public void Update(GameTime time)
        {
            graphic.Update(time);

            if (UseGravity)
            {
                Velocity += new Vector2(0, world.GravityMultiplier * 9.82f * (float)time.ElapsedGameTime.TotalSeconds);
            }

            int oldLeft = (int)previousPosition.X;
            int oldRight = (int)previousPosition.X + BoundingBox.Rectangle.Width;
            int oldTop = (int)previousPosition.Y + BoundingBox.Offset.Y;
            int oldBottom = (int)previousPosition.Y + BoundingBox.Rectangle.Height;

            previousPosition = BoundingBox.Rectangle;
            Position += Velocity;

            if (UseCollisions && world != null && OnCollision != null) //check for collisions
            {
                for (int i = 0; i < world.Objects.Count; i++)
                {
                    GameObject otherObject = world.Objects[i];
                    if (otherObject != this)
                    {
                        if (otherObject.UseCollisions)
                        {
                            int xMinSeparation = otherObject.BoundingBox.Rectangle.Width / 2 + BoundingBox.Rectangle.Width / 2;
                            int yMinSeparation = otherObject.BoundingBox.Rectangle.Height / 2 + BoundingBox.Rectangle.Height / 2;

                            int otherLeft = otherObject.BoundingBox.Rectangle.X;
                            int otherRight = otherObject.BoundingBox.Rectangle.X + otherObject.BoundingBox.Rectangle.Width;
                            int otherTop = otherObject.BoundingBox.Rectangle.Y;
                            int otherBottom = otherObject.BoundingBox.Rectangle.Y + otherObject.BoundingBox.Rectangle.Height;

                            int left = (int)BoundingBox.Rectangle.X;
                            int right = (int)BoundingBox.Rectangle.X + BoundingBox.Rectangle.Width;
                            int top = (int)BoundingBox.Rectangle.Y;
                            int bottom = (int)BoundingBox.Rectangle.Y + BoundingBox.Rectangle.Height;

                            bool collidedFromLeft = oldRight <= otherLeft && right > otherLeft;
                            bool collidedFromRight = oldLeft >= otherRight && left < otherRight;
                            bool collidedFromTop = oldBottom <= otherTop && bottom > otherTop;
                            bool collidedFromBottom = oldTop >= otherBottom && top < otherBottom;

                            Point distance = otherObject.BoundingBox.Rectangle.Center - BoundingBox.Rectangle.Center;

                            if (Math.Abs(distance.X) < xMinSeparation && Math.Abs(distance.Y) < yMinSeparation)
                            {
                                OnCollision.Invoke(new Collision(otherObject, collidedFromTop, collidedFromBottom, collidedFromLeft, collidedFromRight));
                            }
                        }
                    }
                }
            }
        }

        public abstract void CustomUpdate(GameTime time);

        public void AssignWorld(World world)
        {
            this.world = world;
        }

        public void RemoveWorld()
        {
            this.world = null;
        }
    }
}
