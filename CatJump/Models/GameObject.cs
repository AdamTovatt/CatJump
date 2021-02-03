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
        public Texture2D CurrentSprite { get { return animation.GetCurrentFrame(); } }

        private Animation animation;

        public GameObject(Texture2D sprite)
        {
            Init(new Animation(new List<Texture2D>() { sprite }));
        }

        public GameObject(Animation animation)
        {
            Init(animation);
        }

        private void Init(Animation animation)
        {
            this.animation = animation;
        }

        public void Update(GameTime time)
        {
            animation.Update(time);
        }
    }
}
