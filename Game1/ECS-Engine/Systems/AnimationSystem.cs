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
            foreach(GameObject go in Scene.GetAllChildren().Where(e => e.HasComponents(CompatibleTypes)))
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    Animator animator = go.GetComponent<Animator>();
                    if (animator.IsMoving)
                    {
                        Vector2 moveDirection = animator.TargetPosition - transform.Position;
                        moveDirection.Normalize();
                        moveDirection *= animator.MoveSpeed;
                        transform.Move(moveDirection);
                    }

                    if (animator.IsSpinning)
                    {
                        transform.Rotate(animator.SpinSpeed);
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
