using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    public enum SceneType: int
    {
        Menu,
        Game
    }

    public static class SceneFactory
    {
        public static Scene Create(SceneType sceneType)
        {
            switch (sceneType)
            {
                case SceneType.Menu:
                    return new MenuScene();
                case SceneType.Game:
                    return new GameScene();
                default:
                    return new Scene(sceneType.ToString());
            }
        }
    }
}
