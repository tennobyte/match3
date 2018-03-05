using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class GameboardEntity : Entity
    {
        public GameboardEntity(string id, Vector2 position)
            : base(id)
        {
            AddComponents(new Transform(position),
                        new Gameboard(8,8,128));
        }
    }
}
