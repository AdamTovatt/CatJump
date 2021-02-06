using System;
using System.Collections.Generic;
using System.Text;

namespace CatJump.Models
{
    public class World
    {
        public List<GameObject> Objects { get; private set; }
        public bool DrawDebug { get; set; }

        public World()
        {
            Objects = new List<GameObject>();
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
