using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class GameScene: Scene
    {
        public GameScene() 
            : base ("game")
        {
            AddEntity(new GameboardEntity("gameboard", new Vector2(128, 32)));
            AddSystem(new TimerSystem());
            AddSystem(new GameboardSystem());
            AddSystem(new AnimationSystem());
            AddSystem(new ControlSystem());
            AddSystem(new GraphicsSystem());
            AddSystem(new ButtonSystem());
            AddSystem(new TextSystem());
            AddSystem(new ScoreSystem());
        }
    }
}
