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

        private World world;
        private Graphic graphic;

        public GameObject() { }

        protected void Init(Graphic graphic, Vector2 position)
        {
            this.graphic = graphic;
            Position = position;
            _boundingBox = BoundingBox.FromGraphic(graphic);
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
                Velocity += new Vector2(0, 9.82f * (float)time.ElapsedGameTime.TotalSeconds);
            }

            Position += Velocity;

            if (UseCollisions && world != null && OnCollision != null) //check for collisions
            {
                foreach (GameObject gameObject in world.Objects)
                {
                    if (gameObject != this)
                    {
                        if (gameObject.UseCollisions)
                        {
                            int xMin = gameObject.BoundingBox.Rectangle.Width / 2 + BoundingBox.Rectangle.Width / 2;
                            int yMin = gameObject.BoundingBox.Rectangle.Height / 2 + BoundingBox.Rectangle.Height / 2;

                            Point distance = gameObject.BoundingBox.Rectangle.Center - BoundingBox.Rectangle.Center;

                            if (distance.X < xMin && distance.Y < yMin)
                            {
                                OnCollision.Invoke(new Collision(gameObject, true, true, true, true));
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
