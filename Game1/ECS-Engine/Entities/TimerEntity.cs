using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class TimerEntity: Entity
    {
        public TimerEntity(string id, Vector2 position)
        : base(id)
        {
            AddComponents(new Transform(position),
                        new TextRenderer("", "Time left: ", ContentHolder.font, 0f),
                        new Timer());
        }
    }
}
