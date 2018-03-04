using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class AnimationSystem: System
    {
        public AnimationSystem()
            : base(typeof(Transform), typeof(Animator))
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach(Entity go in Scene.Entities.Where(e => e.HasComponents(CompatibleTypes)))
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    Animator animator = go.GetComponent<Animator>();
                    if (animator.IsMoving)
                    {
                        Vector2 moveDirection = animator.TargetPosition - transform.Position;
                        //Console.WriteLine(moveDirection);
                        if (moveDirection.Length() <= animator.MoveSpeed/100 + 1)
                        {
                            animator.ToggleMoving();
                            transform.SetPosition(animator.TargetPosition);
                        }
                        else
                        {
                            moveDirection.Normalize();
                            moveDirection *= animator.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            transform.Move(moveDirection);
                        }
                    }

                    if (animator.IsRotating)
                    {
                        transform.Rotate((float)gameTime.ElapsedGameTime.TotalSeconds * animator.SpinSpeed);
                    }
                    else
                    {
                        transform.SetRotation(0);
                    }
                }
            }
        }
    }
}
