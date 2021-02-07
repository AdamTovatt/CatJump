using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public static class BackgroundColor
    {
        private static Color StartColor = new Color(145, 213, 255);
        private static Color EndColor = new Color(82, 115, 196);

        public static Color GetColor(float y)
        {
            return Color.Lerp(StartColor, EndColor, -y / 4000f);
        }
    }
}
