using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Collider : Component
    {
        public Rectangle Rectangle { get; set; }

        public Collider()
        {
            Rectangle = new Rectangle();
        }

        public Collider (Rectangle rect)
        {
            Rectangle = rect;
        }

        public Collider(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }
    }
}
