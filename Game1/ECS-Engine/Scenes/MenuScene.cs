using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ECS_Engine
{
    class MenuScene: Scene
    {
        public MenuScene()
            : base("menu")
        {
            Vector2 position = new Vector2(Game1.Game1.graphics.PreferredBackBufferWidth / 2, Game1.Game1.graphics.PreferredBackBufferHeight / 2);
            var button = AddEntity(EntityFactory.Create(EntityType.Button, position));
            button.GetComponent<SpriteRenderer>().Texture = ContentHolder.playButton;
            button.GetComponent<Button>().SceneToOpen = SceneType.Game;
            var collider = button.GetComponent<Collider>();
            collider.Rectangle = new Rectangle(position.ToPoint().X - ContentHolder.playButton.Width/2,
                                             position.ToPoint().Y - ContentHolder.playButton.Height/2,
                                            ContentHolder.playButton.Width, ContentHolder.playButton.Height);
            collider.IsActive = true;
            AddSystem(new ControlSystem());
            AddSystem(new GraphicsSystem());
            AddSystem(new ButtonSystem());
        }
    }
}
