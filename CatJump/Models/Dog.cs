using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public class Dog : GameObject
    {
        public Dog(ContentManager content, Vector2 position)
        {
            Init(new Graphic(new List<Texture2D>() { content.Load<Texture2D>("sprite_0"), content.Load<Texture2D>("sprite_1") }), position);
        }

        public override void CustomUpdate(GameTime time)
        {
            
        }
    }
}
