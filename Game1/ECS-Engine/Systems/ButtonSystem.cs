using Microsoft.Xna.Framework;
using System.Linq;

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
            var compatibleEntities = Scene.Entities.Where(e => e.HasExactComponents(CompatibleTypes));
            foreach (Entity go in compatibleEntities)
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
                        ECS.Instance.SetScene(button.SceneToOpen);
                    }
                }
            }
        }
    }
}
