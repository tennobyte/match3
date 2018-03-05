using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace ECS_Engine
{
    class ECS
    {
        private Scene currentScene;
        public static ECS Instance;

        public ECS()
        {
            Instance = this;
        }

        public void Init()
        {
            if (currentScene != null)
            {
                foreach (System system in currentScene.Systems)
                {
                    system.Init();
                }
            }
        }

        public void UpdateSystems(GameTime gameTime)
        {
            if (currentScene != null)
            {
                foreach (System system in currentScene.Systems)
                {
                    system.Update(gameTime);
                }
            }
        }

        public void DrawSystems(SpriteBatch spriteBatch)
        {
            if (currentScene != null)
            {
                foreach (System system in currentScene.Systems)
                {
                    system.Draw(spriteBatch);
                }
            }
        }

        public Scene GetCurrentScene()
        {
            return currentScene;
        }

        public void SetScene(SceneType scene)
        {
            currentScene = SceneFactory.Create(scene);
            Init();
        }
    }
}
