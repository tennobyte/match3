using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class TextRenderer : Component
    {
        public string Text { get; set; }
        public SpriteFont SpriteFont { get; protected set; }

        public TextRenderer()
        {
            Text = "";
        }

        public TextRenderer(string text, SpriteFont font)
        {
            Text = text;
            SpriteFont = font;
        }

        public void SetFont(SpriteFont font)
        {
            SpriteFont = font;
        }
    }
}
