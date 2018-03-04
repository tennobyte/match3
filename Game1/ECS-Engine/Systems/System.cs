using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ECS_Engine
{
    abstract class System: ISystem
    {
        //public List<Entity> CompatibleEntities { get; set; }
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
