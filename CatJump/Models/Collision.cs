using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public class Collision
    {
        public GameObject OtherObject { get; private set; }
        public bool Top { get; private set; }
        public bool Bottom { get; private set; }
        public bool Left { get; private set; }
        public bool Right { get; private set; }

        public Collision(GameObject otherObject, bool top, bool bottom, bool left, bool right)
        {
            OtherObject = otherObject;
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
    }
}
