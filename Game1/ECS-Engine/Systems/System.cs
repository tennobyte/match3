﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ECS_Engine
{
    public abstract class System
    {
        protected List<Type> CompatibleTypes { get; private set; }
        public Scene Scene { get; set; }

        public System(params Type[] types)
        {
            CompatibleTypes = new List<Type>();
            CompatibleTypes.AddRange(types);
        }

        public virtual void Init()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
