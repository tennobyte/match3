using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class ScoreSystem : System
    {
        public ScoreSystem()
            : base(typeof(Transform), typeof(TextRenderer), typeof(Score))
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (Entity go in Scene.Entities.Where(e => e.HasExactComponents(CompatibleTypes)))
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    TextRenderer textRenderer = go.GetComponent<TextRenderer>();
                    Score score = go.GetComponent<Score>();

                    int ScoreMultiplier = 1;

                    if (false)
                    {
                        score.IncreaseScore(10 * ScoreMultiplier);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity go in Scene.Entities.Where(e => e.HasExactComponents(CompatibleTypes)))
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    TextRenderer textRenderer = go.GetComponent<TextRenderer>();
                    Score score = go.GetComponent<Score>();

                    string textToDraw = textRenderer.Text + score.ScoreValue.ToString();
                    spriteBatch.DrawString(textRenderer.SpriteFont, textToDraw, transform.Position, Color.White,transform.Rotation,
                        new Vector2(),transform.Scale,SpriteEffects.None, textRenderer.LayerDepth);
                }
            }
        }
    }
}
