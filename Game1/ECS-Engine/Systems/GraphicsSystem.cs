﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class GraphicsSystem: System
    {
        public GraphicsSystem()
            : base(typeof(Transform), typeof(SpriteRenderer))
        {

        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            foreach (GameObject go in Scene.GetAllChildren().Where(e => e.HasComponents(CompatibleTypes)))
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
                    Vector2 anchor = new Vector2(sprite.Texture.Width / 2, sprite.Texture.Height / 2);
                    if (!sprite.IsTilable)
                    {
                        spriteBatch.Draw(sprite.Texture, transform.Position, null, Color.White,
                            transform.Rotation, anchor, transform.Scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }
    }
}
