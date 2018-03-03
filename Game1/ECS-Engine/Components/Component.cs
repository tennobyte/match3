using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    abstract class Component: IComponent
    {
        public GameObject gameObject { get; set; }
    }
}
