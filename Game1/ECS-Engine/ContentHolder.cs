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
        public static Dictionary<Gems, Texture2D> gemTextures;

        public static void Init(ContentManager content)
        {
            gemTextures = new Dictionary<Gems, Texture2D>();
            for (int i = 0; i < Enum.GetNames(typeof(Gems)).Length; i++)
            {
                gemTextures.Add((Gems)i, content.Load<Texture2D>(Enum.GetName(typeof(Gems), i)));
            }
        }
    }
}
