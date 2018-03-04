﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{

    class Entity : IEntity
    {
        public string Id { get; private set; }
        public bool IsActive { get; private set; }

        public List<Entity> Children { get; private set; }

        public List<IComponent> Components { get; private set; }
        public Entity Parent { get; set; }

        public Scene Scene { get; set; }

        public Entity(string id)
        {
            Id = id;
            Components = new List<IComponent>();
            IsActive = true;
            Children = new List<Entity>();
            Parent = null;
            Scene = null;
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
            var childSelector = new Func<Entity, IEnumerable<Entity>>(ent => ent.Children);

            var stack = new Stack<Entity>(Children);
            while (stack.Any())
            {
                var next = stack.Pop();
                yield return next;
                foreach (var child in childSelector(next))
                    stack.Push(child);
            }
        }

        public bool HasComponent(Type componentType)
        {
            var isFound = Components.Any(c => c.GetType() == componentType);
            if (isFound) return true;

            return false;
        }

        public bool HasComponent<T>()
            where T : IComponent
        {

            var component = Components.OfType<T>().FirstOrDefault();

            if (component != null) return true;

            return false;
        }

        public bool HasComponents(IEnumerable<Type> componentTypes)
        {
            foreach (var ct in componentTypes)
            {
                if (!HasComponent(ct)) return false;
            }
            return true;
        }

        public bool HasExactComponents(IEnumerable<Type> componentTypes)
        {
            if (Components.Count != componentTypes.Count())
            {
                return false;
            }
            return HasComponents(componentTypes);
        }

        public IComponent GetComponent(Type componentType)
        {
            var match = Components.FirstOrDefault(c => c.GetType() == componentType);
            if (match != null)
            {
                return match;
            }

            return null;
        }

        public T GetComponent<T>()
            where T : IComponent
        {

            var match = Components.OfType<T>().FirstOrDefault();

            if (match != null) return match;

            return default(T);
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

        public void RemoveComponent(Type componentType)
        {
            IComponent componentToRemove = GetComponent(componentType);
            if (componentToRemove != null)
            {
                Components.Remove(componentToRemove);
            }
        }

        public void RemoveComponent<T>()
             where T : IComponent
        {
            IComponent componentToRemove = GetComponent<T>();
            if (componentToRemove != null)
            {
                Components.Remove(componentToRemove);
            }
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
