using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class TimerSystem: System
    {
        public TimerSystem()
            : base(typeof(Transform), typeof(TextRenderer), typeof(Timer))
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject go in Scene.GetAllChildren().Where(e => e.HasComponents(CompatibleTypes)))
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    TextRenderer textRenderer = go.GetComponent<TextRenderer>();
                    Timer timer = go.GetComponent<Timer>();

                    if (timer.IsOutOfTime())
                    {
                        //выводим геймовер
                    }
                    else
                    {
                        //Console.WriteLine("TimerSystem: " + (float)gameTime.ElapsedGameTime.TotalSeconds);
                        timer.DecreaseTime((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in Scene.GetAllChildren().Where(e => e.HasComponents(CompatibleTypes)))
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    TextRenderer textRenderer = go.GetComponent<TextRenderer>();
                    Timer timer = go.GetComponent<Timer>();

                    string textToDraw = textRenderer.Text + timer.GetTimeLeft().ToString();
                    spriteBatch.DrawString(textRenderer.SpriteFont, textToDraw, transform.Position, Color.White);
                }
            }
        }
    }
}
