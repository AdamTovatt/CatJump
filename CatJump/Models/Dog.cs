using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            UseCollisions = true;
            UseGravity = true;

            OnCollision += (collision) => { if(collision.Top) Velocity = new Vector2(Velocity.X, -8); };
        }

        public override void CustomUpdate(GameTime time)
        {
            MouseState state =  Mouse.GetState();
            Position = new Vector2(state.X, Position.Y);
        }
    }
}
