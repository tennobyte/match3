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

        float time = 60f;

        public Game1()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Console.WriteLine(new Transform().GetType());
            ecsEngine = new ECS();
            //Content.Load<SpriteFont>("arialFont");
            Scene gameScene = new Scene("GameScene");
            GameObject timer = new GameObject("Timer");
            timer.AddComponents(new Transform(), new TextRenderer("Time left: ", Content.Load<SpriteFont>("arialFont")), new Timer(60));
            gameScene.AddChild(timer);
            gameScene.AddSystem(new TimerSystem());
            ecsEngine.AddScene(gameScene);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Console.WriteLine("MainUpdate: " + (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            ecsEngine.DrawSystems(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
