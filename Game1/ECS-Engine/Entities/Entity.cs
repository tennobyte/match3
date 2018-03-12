using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS_Engine
{

    public abstract class Entity
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public bool IsActive { get; private set; } = true;

        public List<Entity> Children { get; private set; }

        public List<IComponent> Components { get; private set; }
        public Entity Parent { get; set; }

        public Scene Scene { get; set; }

        public Entity()
        {
            Components = new List<IComponent>();
            Children = new List<Entity>();
        }

        public Entity(string id): this()
        {
            Id = id;
        }

        public Entity AddChild(Entity entity)
        {
            entity.Parent = this;
            Children.Add(entity);

            return entity;
        }

        public Entity GetChild(string id)
        {
            return Children.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Entity> GetAllChildren()
        {
            return Children.SelectMany(c => GetAllChildren());
        }

        public bool HasComponent(Type componentType)
        {
            return Components.Any(c => c.GetType() == componentType);
        }

        public bool HasComponent<T>()
            where T : IComponent
        {
            return Components.OfType<T>().Any();
        }

        public bool HasComponents(IEnumerable<Type> componentTypes)
        {
            return componentTypes.All(c => HasComponent(c));
        }

        public bool HasExactComponents(IEnumerable<Type> componentTypes)
        {
            return Components.Count == componentTypes.Count() && 
                componentTypes.All(c => HasComponent(c));
        }

        public IComponent GetComponent(Type componentType)
        {
            return Components.FirstOrDefault(c => c.GetType() == componentType);
        }

        public T GetComponent<T>()
            where T : IComponent
        {
            return Components.OfType<T>().FirstOrDefault();
        }

        public IComponent AddComponent(IComponent component)
        {
            Components.Add(component);
            return component;
        }

        public void AddComponents(IEnumerable<IComponent> components)
        {
            foreach (var component in components)
            {
                AddComponent(component);
            }
        }

        public void AddComponents(params IComponent[] components)
        {
            foreach (var component in components)
            {
                AddComponent(component);
            }
        }

        public bool RemoveComponent(Type componentType)
        {
            return Components.Remove(GetComponent(componentType));
        }

        public bool RemoveComponent<T>()
             where T : IComponent
        {
            return Components.Remove(GetComponent<T>());
        }

        public void RemoveAllComponents()
        {
            Components.Clear();
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
