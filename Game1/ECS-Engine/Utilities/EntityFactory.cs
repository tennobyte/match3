using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    public enum EntityType: int
    {
        Element,
        Timer,
        Score,
        Tile,
        Background,
        Button,
        Gameboard
    }

    public static class EntityFactory
    {
        private static int id = 0;

        public static Entity Create(EntityType typeName, Vector2 position)
        {
            id++;
            switch (typeName)
            {
                case EntityType.Element:
                    return new ElementEntity(id.ToString(), position);
                case EntityType.Timer:
                    return new TimerEntity(id.ToString(), position);
                case EntityType.Score:
                    return new ScoreEntity(id.ToString(), position);
                case EntityType.Tile:
                    return new TileEntity(id.ToString(), position);
                case EntityType.Background:
                    return new BackgroundEntity(id.ToString(), position);
                case EntityType.Button:
                    return new ButtonEntity(id.ToString(), position);
                case EntityType.Gameboard:
                    return new GameboardEntity(id.ToString(), position);
                default:
                    return new Entity(id.ToString());
            }
        }
    }
}
