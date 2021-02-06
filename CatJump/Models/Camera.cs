using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{    public class Camera
    {
        public Matrix Transform { get; private set; }
        public bool LockX { get; set; }
        public bool LockY { get; set; }

        public void Follow(GameObject target)
        {
            Matrix position = Matrix.CreateTranslation(LockX ? 0 : -target.Position.X, LockY ? 0 : -target.Position.Y, 0);
            Matrix offset = Matrix.CreateTranslation(LockX ? 0 : CatJumpGame.ScreenWidth / 2, LockY ? 0 : CatJumpGame.ScreenHeight / 2, 0);

            Transform = position * offset;
        }
    }
}
