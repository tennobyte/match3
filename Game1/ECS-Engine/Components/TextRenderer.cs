using Microsoft.Xna.Framework.Graphics;

namespace ECS_Engine
{
    class TextRenderer : Component
    {
        public string PersistentText { get; set; } = "";
        public string Text { get; set; } = "";
        public SpriteFont SpriteFont { get; private set; }
        public float LayerDepth { get; set; } = 0f;

        public TextRenderer()
        {

        }

        public TextRenderer(string text, SpriteFont font)
        {
            Text = text;
            SpriteFont = font;
        }

        public TextRenderer(string text, string persistentText, SpriteFont font)
        {
            Text = text;
            PersistentText = persistentText;
            SpriteFont = font;
        }

        public TextRenderer(SpriteFont font)
        {
            SpriteFont = font;
        }

        public TextRenderer(string text, SpriteFont font, float layerDepth)
        {
            Text = text;
            SpriteFont = font;
            LayerDepth = layerDepth;
        }

        public TextRenderer(string text, string persistentText, SpriteFont font, float layerDepth)
        {
            Text = text;
            PersistentText = persistentText;
            SpriteFont = font;
            LayerDepth = layerDepth;
        }

        public void SetFont(SpriteFont font)
        {
            SpriteFont = font;
        }
    }
}
