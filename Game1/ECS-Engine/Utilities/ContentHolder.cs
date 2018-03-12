using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS_Engine
{
    static class ContentHolder
    {
        public static Dictionary<Gem, Texture2D> GemTextures { get; private set; }

        public static Texture2D OkButton { get; private set; }
        public static Texture2D PlayButton { get;private set; }
        public static Texture2D GameOverBackground { get; private set; }
        public static Texture2D BoardTile { get; private set; }

        public static SpriteFont font;

        public static void Init(ContentManager content)
        {
            GemTextures = new Dictionary<Gem, Texture2D>();

            var values = Enum.GetValues(typeof(Gem)).OfType<Gem>();
            GemTextures = values.ToDictionary(k => k, d => content.Load<Texture2D>(d.ToString()));

            OkButton = content.Load<Texture2D>("OkButton");
            PlayButton = content.Load<Texture2D>("PlayButton");
            font = content.Load<SpriteFont>("arialFont");
            GameOverBackground = content.Load<Texture2D>("GameOver");
            BoardTile = content.Load<Texture2D>("Tile");
        }
    }
}
