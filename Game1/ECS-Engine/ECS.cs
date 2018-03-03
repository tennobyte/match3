using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace ECS_Engine
{
    class ECS
    {
        //List<IComponent> componentList = new List<IComponent>();
        //List<IEntity> entityList = new List<IEntity>();
        //List<ISystem> systemList = new List<ISystem>();
        public enum ComponentName
        {
            Transform,
            SpriteRenderer,
            Animator
        }

        public List<Scene> GameScenes { get; private set; }
        private int currentSceneNumber;
        public static ECS Instance;

        public ECS()
        {
            GameScenes = new List<Scene>();
            currentSceneNumber = -1;
            Instance = this;
        }

        public void UpdateSystems(GameTime gameTime)
        {
            Console.WriteLine("ECS: " + (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (currentSceneNumber >= 0)
            {
                foreach (System system in GameScenes[currentSceneNumber].Systems)
                {
                    
                    system.Update(gameTime);
                }
            }
        }

        public void DrawSystems(SpriteBatch spriteBatch)
        {
            if (currentSceneNumber >= 0)
            {
                foreach (System system in GameScenes[currentSceneNumber].Systems)
                {
                    system.Draw(spriteBatch);
                }
            }
        }

        public void AddScene(Scene scene)
        {
            if(GameScenes.Count == 0)
            {
                currentSceneNumber = 0;
            }
            GameScenes.Add(scene);
        }

        public void DeleteScene(Scene scene)
        {
            if (GameScenes.IndexOf(scene) == currentSceneNumber)
            {
                currentSceneNumber = 0;
            }
            GameScenes.Remove(scene);
            if (GameScenes.Count == 0)
            {
                currentSceneNumber = -1;
            }
        }

        public void DeleteScene(string sceneName)
        {
            Scene sceneToDelete = GameScenes.Find(gs => gs.Id == sceneName);
            if(sceneToDelete != null)
            {
                if (GameScenes.IndexOf(sceneToDelete) == currentSceneNumber)
                {
                    currentSceneNumber = 0;
                }
                GameScenes.Remove(sceneToDelete);
            }
            if (GameScenes.Count == 0)
            {
                currentSceneNumber = -1;
            }
        }

        public void DeleteScene(int sceneNumber)
        {
            GameScenes.RemoveAt(sceneNumber);
            if(sceneNumber == currentSceneNumber)
            {
                currentSceneNumber = 0;
            }
        }

        public void SwitchScene(int sceneNumber)
        {
            if(GameScenes.Count > sceneNumber && sceneNumber >= 0)
                currentSceneNumber = sceneNumber;
        }

        public void SwitchScene(string sceneName)
        {
            int sceneIndex = GameScenes.FindIndex(gs => gs.Id == sceneName);
            if (sceneIndex != -1)
                currentSceneNumber = sceneIndex;
        }

        public void SwitchScene(Scene scene)
        {
            int sceneIndex = GameScenes.IndexOf(scene);
            if (sceneIndex != -1)
                currentSceneNumber = sceneIndex;
        }

        public void NextScene()
        {
            if(currentSceneNumber + 1 == GameScenes.Count)
            {
                currentSceneNumber = 0;
            }
            else
            {
                currentSceneNumber++;
            }
        }

        public void Test()
        {
            GameObject myEntity = new GameObject("boop");
            myEntity.AddComponent(new Transform());
            myEntity.AddComponents(new Transform(), new SpriteRenderer());

            List<IComponent> list = new List<IComponent>();
            list.Add(new Transform() {});
            list.Add(new SpriteRenderer());
            list.Add(new Animator());
            myEntity.AddComponents(list);


            myEntity.GetComponent<Transform>();
            myEntity.GetComponent(new Transform().GetType());

            myEntity.HasComponent(new Transform().GetType());
            myEntity.HasComponent<Transform>();

            myEntity.RemoveComponent<Transform>();
            myEntity.RemoveComponent(new Transform().GetType());
            myEntity.RemoveAllComponents();
        }
    }
}
