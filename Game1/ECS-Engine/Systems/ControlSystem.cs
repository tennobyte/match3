using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace ECS_Engine
{
    class ControlSystem : System
    {
        private MouseState prevousState;
        private float controlTimeOut = 0.2f;

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
            if(controlTimeOut <= 0)
            {
                var compatibleEntities = Scene.Entities.Where(e => e.HasComponents(CompatibleTypes))
                    .Where(e => e.GetComponent<Collider>().IsActive);
                foreach (Entity go in compatibleEntities)
                {
                    var collider = go.GetComponent<Collider>();
                    var mousePoint = new Point(state.X, state.Y);
                    if (collider.Rectangle.Contains(mousePoint))
                    {
                        collider.IsHovered = true;
                        if (state.LeftButton == ButtonState.Pressed && prevousState.LeftButton != ButtonState.Pressed)
                        {
                            collider.IsClicked = true;
                        }
                    }
                    else
                    {
                        collider.ResetStatus();
                    }
                }
                prevousState = state;
            }
            else
            {
                controlTimeOut -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
