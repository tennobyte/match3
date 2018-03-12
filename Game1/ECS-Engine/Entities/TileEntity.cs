using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class TileEntity: Entity
    {
        public TileEntity(string id, Vector2 position)
            : base(id)
        {
            AddComponents(new Transform(position),
                        new SpriteRenderer(ContentHolder.BoardTile, ContentHolder.BoardTile.Width / 2, 8, 8, 0.5f));
        }
    }
}
