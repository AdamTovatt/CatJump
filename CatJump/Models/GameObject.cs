using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public class GameObject
    {
        public bool Visible { get; set; } = true;
        public bool UseGravity { get; set; }
        public Texture2D CurrentSprite { get { return animation.GetCurrentFrame(); } }
        public BoundingBox BoundingBox { get { return GetBoundingBox(); } }
        private BoundingBox _boundingBox;
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 SpritePosition { get { return Position - Origin; } }
        public Vector2 Origin { get; private set; }

        private Animation animation;

        public GameObject(Texture2D sprite, Vector2 position = default(Vector2))
        {
            Init(new Animation(new List<Texture2D>() { sprite }), position);
        }

        public GameObject(Animation animation, Vector2 position = default(Vector2))
        {
            Init(animation, position);
        }

        private void Init(Animation animation, Vector2 position)
        {
            this.animation = animation;
            Position = position;
            _boundingBox = BoundingBox.FromAnimation(animation);
            Origin = new Vector2(_boundingBox.Rectangle.Width / 2 + _boundingBox.Offset.X / 2, _boundingBox.Rectangle.Height / 2 + _boundingBox.Offset.Y / 2);
        }

        private BoundingBox GetBoundingBox()
        {
            Point newPosition = new Point((int)SpritePosition.X + _boundingBox.Offset.X, (int)SpritePosition.Y + _boundingBox.Offset.Y);
            _boundingBox.Rectangle = new Rectangle(newPosition, _boundingBox.Rectangle.Size);
            return _boundingBox;
        }

        public void Update(GameTime time)
        {
            animation.Update(time);

            if (UseGravity)
            {
                Velocity += new Vector2(0, 9.82f * (float)time.ElapsedGameTime.TotalSeconds);
            }

            Position += Velocity;
        }
    }
}
