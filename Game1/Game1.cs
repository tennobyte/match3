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
            // TODO: Add your initialization logic here
            Console.WriteLine(new Transform().GetType());
            ecsEngine = new ECS();
            //Content.Load<SpriteFont>("arialFont");
            Scene gameScene = new Scene("gamescene");

            Entity timer = new Entity("timer");
            timer.AddComponents(new Transform(new Vector2(5,20)), new TextRenderer("Time left: ", Content.Load<SpriteFont>("arialFont"), 0f), new Timer(60));
            gameScene.AddEntity(timer);
            gameScene.AddSystem(new TimerSystem());

            Entity score = new Entity("score");
            score.AddComponents(new Transform(new Vector2(5,60)), new TextRenderer("Score: ", Content.Load<SpriteFont>("arialFont"), 0f), new Score());
            gameScene.AddEntity(score);
            gameScene.AddSystem(new ScoreSystem());

            Entity gameBoardTile = new Entity("tile");
            Texture2D tileTexture = Content.Load<Texture2D>("Tile");
            gameBoardTile.AddComponents(new Transform(new Vector2(128,32),0.5f,0), new SpriteRenderer(tileTexture, tileTexture.Width/2,8,8,0.1f));
            gameScene.AddEntity(gameBoardTile);
            gameScene.AddSystem(new GraphicsSystem());

            Entity gameboard = new Entity("gameboard");
            gameboard.AddComponents(new Transform(new Vector2(128, 32)), new Gameboard(8,8,tileTexture.Width / 2));
            gameScene.AddEntity(gameboard);
            gameScene.AddSystem(new GameboardSystem());
            gameScene.AddSystem(new AnimationSystem());

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
