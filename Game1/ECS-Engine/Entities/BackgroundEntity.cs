using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class BackgroundEntity: Entity
    {
        public BackgroundEntity(string id, Vector2 position)
            : base(id)
        {
            AddComponents(new Transform(position),
                        new SpriteRenderer(ContentHolder.GameOverBackground,0.3f));
        }
    }
}
