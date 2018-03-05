using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class ControlSystem : System
    {
        MouseState prevousState;

        public ControlSystem()
            : base(typeof(Transform), typeof(Collider))
        {

        }

        public override void Init()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();
            
            foreach (Entity go in Scene.Entities.Where(e => e.HasComponents(CompatibleTypes)).Where(e => e.GetComponent<Collider>().IsActive))
            {
                var collider = go.GetComponent<Collider>();
                var mousePoint = new Point(state.X, state.Y);
                if (collider.Rectangle.Contains(mousePoint))
                {
                    //Console.WriteLine("hovered over " + go.Id);
                    collider.IsHovered = true;
                    if (state.LeftButton == ButtonState.Pressed && prevousState.LeftButton != ButtonState.Pressed)
                    {
                        collider.IsClicked = true;
                        Console.WriteLine("clicked on " + go.Id);
                    }
                }
                else
                {
                    collider.ResetStatus();
                }
            }
            prevousState = state;
        }
    }
}
