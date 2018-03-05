using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    static class ContentHolder
    {
        public static Dictionary<Gem, Texture2D> gemTextures;

        public static Texture2D okButton;
        public static Texture2D playButton;
        public static Texture2D gameOverBackground;
        public static Texture2D boardTile;

        public static SpriteFont font;

        public static void Init(ContentManager content)
        {
            gemTextures = new Dictionary<Gem, Texture2D>();
            for (int i = 0; i < Enum.GetNames(typeof(Gem)).Length; i++)
            {
                gemTextures.Add((Gem)i, content.Load<Texture2D>(Enum.GetName(typeof(Gem), i)));
            }
            okButton = content.Load<Texture2D>("OkButton");
            playButton = content.Load<Texture2D>("PlayButton");
            font = content.Load<SpriteFont>("arialFont");
            gameOverBackground = content.Load<Texture2D>("GameOver");
            boardTile = content.Load<Texture2D>("Tile");
        }
    }
}
