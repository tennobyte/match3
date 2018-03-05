using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class ButtonEntity: Entity
    {
        public ButtonEntity(string id, Vector2 position)
            : base(id)
        {
            AddComponents(new Transform(position),
                        new SpriteRenderer(),
                        new Collider(),
                        new Button());
            GetComponent<SpriteRenderer>().LayerDepth = 0.2f;
        }
    }
}
