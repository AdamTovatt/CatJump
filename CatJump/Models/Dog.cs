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
        private const int jumpStrength = 16;

        public Dog(ContentManager content, Vector2 position)
        {
            Init(new Graphic(new List<Texture2D>() { content.Load<Texture2D>("sprite_0"), content.Load<Texture2D>("sprite_1") }), position);
            UseCollisions = true;
            UseGravity = true;

            Velocity = new Vector2(0, -jumpStrength);

            OnCollision += (collision) => { OnCollided(collision); };
        }

        private void OnCollided(Collision collision)
        {
            if (collision.OtherObject.GetType() == typeof(Block) && collision.Top)
            {
                Velocity = new Vector2(Velocity.X, -jumpStrength);
                world.UpdateBlocks(Position.Y);
            }
        }

        public override void CustomUpdate(GameTime time)
        {
            MouseState state = Mouse.GetState();
            Position = new Vector2(state.X, Position.Y);
        }
    }
}
