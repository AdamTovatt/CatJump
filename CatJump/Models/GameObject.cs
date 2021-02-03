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
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }

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
        }

        public void Update(GameTime time)
        {
            animation.Update(time);
            
            if(UseGravity)
            {
                Velocity += new Vector2(0, 9.82f * (float)time.ElapsedGameTime.TotalSeconds);
            }

            Position += Velocity;
        }
    }
}
