using CatJump.ExtensionMethods;
using CatJump.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CatJump
{
    public class CatJumpGame : Game
    {
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static Texture2D pixel;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera camera;
        private World world;
        private Dog player;

        public CatJumpGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;

            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            world = new World(Content, ScreenWidth, ScreenHeight, 2);
            world.DrawDebug = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera() { LockX = true };

            IsMouseVisible = false;

            player = new Dog(Content, new Vector2(ScreenWidth / 2, 100));
            world.AddObject(player);
            world.AddObject(new Block(Content, new Vector2(player.Position.X, 150)));
            world.UpdateBlocks(player.Position.Y);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < world.Objects.Count; i++)
            {
                world.Objects[i].Update(gameTime);
                world.Objects[i].CustomUpdate(gameTime);
            }

            camera.Follow(player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: camera.Transform);

            for (int i = 0; i < world.Objects.Count; i++)
            {
                GameObject gameObject = world.Objects[i];

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
