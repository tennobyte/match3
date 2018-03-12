using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS_Engine
{
    class SpriteRenderer : Component
    {
        public Texture2D Texture { get; set; } 
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
        public int AlphaValue { get; private set; } = 255;
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

        public void DecreaseAlpha(float value)
        {
            AlphaValue = MathHelper.Clamp((int)(AlphaValue - value), 0, 255);
        }

        public void ResetAlpha()
        {
            AlphaValue = 255;
        }

        public void SetAlpha(int value)
        {
            AlphaValue = MathHelper.Clamp(value, 0, 255);
        }
    }
}
