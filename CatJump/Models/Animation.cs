using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public class Animation
    {
        public bool Playing { get; set; } = true;
        public double FrameRate { get { return frameRate; } set { frameDelay = 1000 / value; frameRate = value; } }
        private double frameRate = 16;

        private List<Texture2D> sprites;
        private int currentFrame = 0;
        private double passedTime = 0;
        private double frameDelay = 1000 / 16;

        public Animation(List<Texture2D> sprites)
        {
            this.sprites = sprites;
        }

        public void Update(GameTime time)
        {
            if (time.ElapsedGameTime.TotalMilliseconds + passedTime > frameDelay)
            {
                passedTime = 0;
                currentFrame++;
                if (currentFrame >= sprites.Count)
                {
                    currentFrame = 0;
                }
            }
            else
            {
                passedTime += time.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public Texture2D GetCurrentFrame()
        {
            return sprites[currentFrame];
        }
    }
}
