using Microsoft.Xna.Framework;
using System.Linq;

namespace ECS_Engine
{
    class AnimationSystem: System
    {
        public AnimationSystem()
            : base(typeof(Transform), typeof(Animator), typeof(SpriteRenderer), typeof(Collider))
        {

        }

        public override void Update(GameTime gameTime)
        {
            var compatibleEntities = Scene.Entities.Where(e => e.HasComponents(CompatibleTypes));
            foreach (Entity go in compatibleEntities)
            {
                if (go.IsActive)
                {
                    Transform transform = go.GetComponent<Transform>();
                    Animator animator = go.GetComponent<Animator>();
                    SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                    Collider collider = go.GetComponent<Collider>();
                    if (animator.IsMoving)
                    {
                        Vector2 moveDirection = animator.TargetPosition - transform.Position;
                        if (moveDirection.Length() <= animator.MoveSpeed/100 + 1)
                        {
                            animator.ToggleMoving();
                            transform.SetPosition(animator.TargetPosition);
                            collider.RefreshPosition((int)(transform.Position.X 
                                - spriteRenderer.Texture.Width/2 * transform.Scale),
                                (int)(transform.Position.Y 
                                - spriteRenderer.Texture.Height / 2 * transform.Scale));
                        }
                        else
                        {
                            moveDirection.Normalize();
                            moveDirection *= animator.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            transform.Move(moveDirection);
                        }
                    }

                    if (animator.IsFading)
                    {
                        spriteRenderer.DecreaseAlpha((float)gameTime.ElapsedGameTime.TotalSeconds * animator.FadeSpeed);
                        if (spriteRenderer.AlphaValue < 1)
                        {
                            animator.ToggleFading();
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
