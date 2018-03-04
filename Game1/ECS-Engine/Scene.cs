﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Scene
    {
        public List<System> Systems { get; private set; }
        public List<Entity> Entities { get; private set; }
        public string Id { get; private set; }

        public Scene(string id)
        {
            Id = id;
            //IsActive = false;
            Entities = new List<Entity>();
            Systems = new List<System>();
        }

        public void AddSystem(System system)
        {
            Systems.Add(system);
            system.Scene = this;
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
            entity.Scene = this;
            AddEntities(entity.GetAllChildren());
        }

        private void AddEntities(IEnumerable<Entity> entities)
        {
            foreach(Entity entity in entities)
            {
                Entities.Add(entity);
                entity.Scene = this;
            }
        }

        public Entity FindEntity(string entityId)
        {
            return Entities.FirstOrDefault(e => e.Id == entityId);
        }
    }
}
