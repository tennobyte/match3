using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class ScoreEntity:Entity
    {
        public ScoreEntity(string id, Vector2 position)
            : base(id)
        {
            AddComponents(new Transform(position),
                        new TextRenderer("", "Score: ", ContentHolder.font, 0f),
                        new Score());
        }
    }
}
