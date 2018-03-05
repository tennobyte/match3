using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class ElementEntity: Entity
    {
        public ElementEntity(string id, Vector2 position)
            : base(id)
        {
            AddComponents(new Transform(position), 
                        new GameboardElement(),
                        new SpriteRenderer(ContentHolder.gemTextures[0], 0.4f),
                        new Animator(),
                        new Collider());
        }
    }
}
