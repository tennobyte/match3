using Microsoft.Xna.Framework;
using System.Linq;

namespace ECS_Engine
{
    class ScoreSystem: System
    {
        public ScoreSystem()
            : base(typeof(TextRenderer), typeof(Score))
        {

        }

        public override void Update(GameTime gameTime)
        {
            var compatibleEntities = Scene.Entities.Where(e => e.HasComponents(CompatibleTypes));
            foreach (Entity go in compatibleEntities)
            {
                if (go.IsActive)
                {
                    TextRenderer textRenderer = go.GetComponent<TextRenderer>();
                    Score score = go.GetComponent<Score>();
                    textRenderer.Text = score.ScoreValue.ToString();
                }
            }
        }
    }
}
