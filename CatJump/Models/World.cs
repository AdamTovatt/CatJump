using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatJump.Models
{
    public class World
    {
        public List<GameObject> Objects { get; private set; }
        public ContentManager ContentManager { get; private set; }
        public bool DrawDebug { get; set; }
        public int ScreenHeight { get; set; }
        public int BlockSeparation { get; set; } = 100;
        public int ScreenWidth { get; private set; }
        public float GravityMultiplier { get; set; }

        private Random random;
        private const int blockPlacementMargin = 10;

        public World(ContentManager contentManager, int screenWidth, int screenHeight, float gravityMultiplier)
        {
            Objects = new List<GameObject>();
            random = new Random();

            ContentManager = contentManager;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            GravityMultiplier = gravityMultiplier;
        }

        public void UpdateBlocks(float playerY)
        {
            List<GameObject> blocks = Objects.Where(x => x.GetType() == typeof(Block)).ToList();

            float topBlockY = blocks.First().Position.Y;

            foreach (Block block in blocks)
            {
                if(block.Position.Y > playerY + (ScreenHeight / 3))
                {
                    RemoveObject(block);
                }
                else
                {
                    if(block.Position.Y < topBlockY)
                    {
                        topBlockY = block.Position.Y;
                    }
                }
            }

            while(topBlockY > playerY - (ScreenHeight * 3))
            {
                float newY = topBlockY - BlockSeparation;

                Block block = new Block(ContentManager, new Vector2(-100, newY));
                float blockThickness = block.BoundingBox.Rectangle.Width / 2;

                float newX = random.Next((int)blockThickness + blockPlacementMargin, (int)(ScreenWidth - blockThickness - blockPlacementMargin));

                block.Position = new Vector2(newX, newY);

                AddObject(block);
                topBlockY = newY;
            }
        }

        public void AddObject(GameObject gameObject)
        {
            gameObject.AssignWorld(this);
            Objects.Add(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            gameObject.RemoveWorld();
            Objects.Remove(gameObject);
        }
    }
}
