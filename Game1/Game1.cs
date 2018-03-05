using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ECS_Engine;
using System;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ECS ecsEngine;

        //float time = 60f;

        public Game1()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 512;
            graphics.PreferredBackBufferWidth = 608;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ecsEngine = new ECS();
            Scene gameScene = new Scene("gamescene");
            gameScene.AddSystem(new TimerSystem());
            gameScene.AddSystem(new ScoreSystem());
            gameScene.AddSystem(new GameboardSystem());
            gameScene.AddSystem(new AnimationSystem());
            gameScene.AddSystem(new ControlSystem());
            gameScene.AddSystem(new GraphicsSystem());

            Entity gameboard = new Entity("gameboard");
            gameboard.AddComponents(new Transform(new Vector2(128, 32)), new Gameboard(8,8,128));
            gameScene.AddEntity(gameboard);


            Entity playButton = new Entity("playbutton");
            playButton.AddComponents(new Transform(),
                new Button(),
                new SpriteRenderer(),
                new Collider());

            ecsEngine.AddScene(gameScene);

            //ecsEngine.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentHolder.Init(Content);
            ecsEngine.Init();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            ecsEngine.UpdateSystems(gameTime);

            //time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Console.WriteLine("MainUpdate: " + (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            ecsEngine.DrawSystems(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
