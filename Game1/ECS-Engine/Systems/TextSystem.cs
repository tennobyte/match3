using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace ECS_Engine
{
    class TextSystem: System
    {
        public TextSystem()
            : base(typeof(Transform), typeof(TextRenderer))
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var compatibleEntities = Scene.Entities.Where(e => e.HasComponents(CompatibleTypes));
            foreach (Entity go in compatibleEntities)
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    TextRenderer textRenderer = go.GetComponent<TextRenderer>();
                    var textToDraw = textRenderer.PersistentText + textRenderer.Text;
                    spriteBatch.DrawString(textRenderer.SpriteFont, textToDraw, transform.Position, 
                        Color.White, transform.Rotation, new Vector2(), 
                        transform.Scale, SpriteEffects.None, textRenderer.LayerDepth);
                }
            }
        }
    }
}
