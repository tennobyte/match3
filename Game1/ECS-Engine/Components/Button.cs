using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Button: Component
    {
        public string Data { get; set; }
        public bool IsHighlighted { get; set; }

        public Button()
        {
            IsHighlighted = false;
        }

        public Button(string data)
        {
            Data = data;
            IsHighlighted = false;
        }

    }
}
