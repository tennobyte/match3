using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class SpriteRenderer : Component
    {
        public Texture2D Texture { get; set; }
        public bool IsTilable { get; set; } = false;
        public float Spacing { get; set; } = 0;
        public int VerticalRepeat { get; set; } = 0;
        public int HorizontalRepeat { get; set; } = 0;
    }
}
