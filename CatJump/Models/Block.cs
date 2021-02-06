using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public class Block : GameObject
    {
        public Block(ContentManager content, Vector2 position)
        {
            Init(new Graphic(content.Load<Texture2D>("block_1")), position);
            UseCollisions = true;
        }

        public override void CustomUpdate(GameTime time) { }
    }
}
