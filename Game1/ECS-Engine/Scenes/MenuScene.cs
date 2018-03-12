using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class MenuScene: Scene
    {
        public MenuScene()
            : base("menu")
        {
            Vector2 position = new Vector2(Game1.Game1.graphics.PreferredBackBufferWidth / 2, Game1.Game1.graphics.PreferredBackBufferHeight / 2);
            var button = AddEntity(new ButtonEntity("playbutton",position));
            button.GetComponent<SpriteRenderer>().Texture = ContentHolder.PlayButton;
            button.GetComponent<Button>().SceneToOpen = SceneType.Game;
            var collider = button.GetComponent<Collider>();
            collider.Rectangle = new Rectangle(position.ToPoint().X - ContentHolder.PlayButton.Width/2,
                                             position.ToPoint().Y - ContentHolder.PlayButton.Height/2,
                                            ContentHolder.PlayButton.Width, ContentHolder.PlayButton.Height);
            collider.IsActive = true;
            AddSystem(new ControlSystem());
            AddSystem(new GraphicsSystem());
            AddSystem(new ButtonSystem());
        }
    }
}
