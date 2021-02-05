using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public class Graphic
    {
        public bool Animated { get; set; } = true;
        public double FrameRate { get { return frameRate; } set { frameDelay = 1000 / value; frameRate = value; } }
        private double frameRate = 16;
        public List<Texture2D> Sprites { get; private set; }

        private int currentFrame = 0;
        private double passedTime = 0;
        private double frameDelay = 1000 / 16;

        public Graphic(List<Texture2D> sprites)
        {
            Sprites = sprites;
        }

        public Graphic(Texture2D sprite)
        {
            Sprites = new List<Texture2D>() { sprite };
            Animated = false;
        }

        public void Update(GameTime time)
        {
            if (time.ElapsedGameTime.TotalMilliseconds + passedTime > frameDelay)
            {
                passedTime = 0;
                currentFrame++;
                if (currentFrame >= Sprites.Count)
                {
                    currentFrame = 0;
                }
            }
            else
            {
                passedTime += time.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public Texture2D GetCurrentSprite()
        {
            return Sprites[currentFrame];
        }
    }
}
