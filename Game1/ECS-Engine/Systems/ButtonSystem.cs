using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class ButtonSystem : System
    {
        public ButtonSystem()
            : base(typeof(Transform), typeof(SpriteRenderer), typeof(Button), typeof(Collider))
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (Entity go in Scene.Entities.Where(e => e.HasExactComponents(CompatibleTypes)))
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                    Button button = go.GetComponent<Button>();
                    Collider collider = go.GetComponent<Collider>();

                    if (collider.IsHovered)
                    {
                        spriteRenderer.SetAlpha(150);
                    }
                    else
                    {
                        spriteRenderer.ResetAlpha();
                    }
                    if (collider.IsClicked)
                    {
                        spriteRenderer.ResetAlpha();
                        Console.WriteLine(button.SceneToOpen);
                        ECS.Instance.SetScene(button.SceneToOpen);
                    }
                }
            }
        }
    }
}
