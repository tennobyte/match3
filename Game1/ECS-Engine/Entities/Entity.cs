using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{

    abstract class Entity : IEntity
    {
        public string Id { get; set; }
        public bool IsActive { get; protected set; }

        public List<GameObject> Children { get; set; }

        

        public GameObject AddChild(GameObject entity)
        {
            entity.Parent = this;
            Children.Add(entity);

            return entity;
        }

        public GameObject GetChild(string id)
        {
            return Children.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<GameObject> GetAllChildren()
        {
            var childSelector = new Func<GameObject, IEnumerable<GameObject>>(ent => ent.Children);

            var stack = new Stack<GameObject>(Children);
            while (stack.Any())
            {
                var next = stack.Pop();
                yield return next;
                foreach (var child in childSelector(next))
                    stack.Push(child);
            }
        }

        //public Entity(string id)
        //{
        //    Id = id;

        //    IsActive = true;
        //}



        //public transform transform
        //{
        //    get { return this.getcomponent<transform>(); }
        //}
    }
}
