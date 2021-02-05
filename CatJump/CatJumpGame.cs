﻿using CatJump.ExtensionMethods;
using CatJump.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CatJump
{
    public class CatJumpGame : Game
    {
        public static Texture2D pixel;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private World world;

        public CatJumpGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            world = new World();
            world.DrawDebug = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Graphic dogAnimation = new Graphic(new List<Texture2D>() { Content.Load<Texture2D>("sprite_0"), Content.Load<Texture2D>("sprite_1") });
            GameObject dog = new GameObject(dogAnimation);
            dog.UseGravity = true;
            dog.Position = new Vector2(100, 100);

            Graphic blockAnimation = new Graphic(Content.Load<Texture2D>("block_1"));
            GameObject block = new GameObject(blockAnimation);
            block.Position = new Vector2(80, 400);

            world.AddObject(dog);
            world.AddObject(block);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject gameObject in world.Objects)
            {
                gameObject.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (GameObject gameObject in world.Objects)
            {
                if (gameObject.Visible)
                {
                    _spriteBatch.Draw(gameObject.CurrentSprite, gameObject.SpritePosition, Color.White);
                    if (world.DrawDebug)
                    {
                        _spriteBatch.DrawBorder(gameObject.BoundingBox.Rectangle, 2, Color.White);
                        _spriteBatch.Draw(pixel, gameObject.Position, Color.White);
                    }
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
