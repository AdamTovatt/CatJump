using CatJump.ExtensionMethods;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public class BoundingBox
    {
        public Rectangle Rectangle { get; set; }
        public Point Offset { get; set; }

        public BoundingBox(Rectangle rectangle)
        {
            Offset = new Point(rectangle.X, rectangle.Y);
            Rectangle = rectangle;
            Rectangle.Offset(new Point(-1 * Offset.X, -1 * Offset.Y));
        }

        public static BoundingBox FromAnimation(Animation animation)
        {
            if (animation.Sprites.Count == 0)
                return null;

            Texture2D sprite = animation.Sprites[0];

            Color[,] Colors = sprite.To2dArray();

            int x1 = 9999999, y1 = 9999999;
            int x2 = -999999, y2 = -999999;

            for (int a = 0; a < sprite.Width; a++)
            {
                for (int b = 0; b < sprite.Height; b++)
                {
                    if (Colors[a, b].A != 0)
                    {
                        if (x1 > a) x1 = a;
                        if (x2 < a) x2 = a;

                        if (y1 > b) y1 = b;
                        if (y2 < b) y2 = b;
                    }
                }
            }

            return new BoundingBox(new Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1));
        }
    }
}
