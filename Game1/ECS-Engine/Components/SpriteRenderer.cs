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
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
        public bool IsTilable { get; set; } = false;
        public int Spacing { get; set; } = 0;
        public int VerticalRepeat { get; set; } = 0;
        public int HorizontalRepeat { get; set; } = 0;
        public float LayerDepth { get; set; } = 0f;

        public SpriteRenderer()
        {
            
        }

        public SpriteRenderer(Texture2D texture)
        {
            Texture = texture;
        }

        public SpriteRenderer(Texture2D texture, float layerDepth)
        {
            Texture = texture;
            LayerDepth = layerDepth;
        }

        public SpriteRenderer(Texture2D texture, int spacing, int verticalRepeat, int horizontalRepeat)
        {
            Texture = texture;
            IsTilable = true;
            Spacing = spacing;
            VerticalRepeat = verticalRepeat;
            HorizontalRepeat = horizontalRepeat;
        }

        public SpriteRenderer(Texture2D texture, int spacing, int verticalRepeat, int horizontalRepeat, float layerDepth)
        {
            Texture = texture;
            IsTilable = true;
            Spacing = spacing;
            VerticalRepeat = verticalRepeat;
            HorizontalRepeat = horizontalRepeat;
            LayerDepth = layerDepth;
        }
    }
}
