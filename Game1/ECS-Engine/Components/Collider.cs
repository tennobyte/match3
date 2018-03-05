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
        public Rectangle Rectangle { get;  set; }
        public bool IsActive { get; set; } = false;
        public bool IsClicked { get; set; } = false;
        public bool IsHovered { get; set; } = false;

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

        public void RefreshPosition(int x, int y)
        {
            Rectangle = new Rectangle(x, y, Rectangle.Width, Rectangle.Height);
        }

        public void ResetStatus()
        {
            IsClicked = false;
            IsHovered = false;
        }
    }
}
