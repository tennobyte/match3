namespace ECS_Engine
{
    class Button: Component
    {
        public SceneType SceneToOpen { get; set; }

        public Button()
        {

        }

        public Button(SceneType scene)
        {
            SceneToOpen = scene;
        }
    }
}
