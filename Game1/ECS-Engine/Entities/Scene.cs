using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Scene: Entity
    {
        public List<System> Systems { get; private set; }

        public Scene(string id)
        {
            Id = id;
            IsActive = false;
            Children = new List<GameObject>();
            Systems = new List<System>();
        }

        public void AddSystem(System system)
        {
            Systems.Add(system);
            system.Scene = this;
        }
    }
}
