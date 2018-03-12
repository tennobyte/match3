using Microsoft.Xna.Framework;
using System.Linq;

namespace ECS_Engine
{
    class TimerSystem: System
    {
        public TimerSystem()
            : base(typeof(TextRenderer), typeof(Timer))
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
                    Timer timer = go.GetComponent<Timer>();

                    if (!timer.IsOutOfTime())
                    {
                        timer.DecreaseTime((float)gameTime.ElapsedGameTime.TotalSeconds);
                        textRenderer.Text = timer.GetSecondsLeft().ToString();
                    }
                }
            }
        }
    }
}
