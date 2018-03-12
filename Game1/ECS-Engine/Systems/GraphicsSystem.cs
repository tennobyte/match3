using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace ECS_Engine
{
    class GraphicsSystem: System
    {
        public GraphicsSystem()
            : base(typeof(Transform), typeof(SpriteRenderer))
        {

        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            var compatibleEntities = Scene.Entities.Where(e => e.HasComponents(CompatibleTypes));
            foreach (Entity go in compatibleEntities)
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
                    Vector2 anchor = new Vector2(sprite.Texture.Width / 2, sprite.Texture.Height / 2);
                    if (!sprite.IsTilable)
                    {
                        spriteBatch.Draw(sprite.Texture, transform.Position, null, 
                            new Color(255, 255, 255, MathHelper.Clamp(sprite.AlphaValue, 0, 255)),
                            transform.Rotation, anchor, transform.Scale, sprite.SpriteEffects, sprite.LayerDepth);
                    }
                    else
                    {
                        for(int i = 0; i< sprite.VerticalRepeat; i++)
                        {
                            for (int j = 0; j < sprite.HorizontalRepeat; j++)
                            {
                                int spacing = sprite.Spacing;
                                Vector2 newPosition = new Vector2(transform.Position.X + spacing * i, 
                                    transform.Position.Y + spacing * j);
                                spriteBatch.Draw(sprite.Texture, newPosition, null, 
                                    new Color(255,255,255, MathHelper.Clamp(sprite.AlphaValue,0,255)),
                                    transform.Rotation, anchor, transform.Scale, sprite.SpriteEffects, sprite.LayerDepth);
                            }
                        }
                    }
                }
            }
        }
    }
}
