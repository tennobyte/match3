using Microsoft.Xna.Framework;

namespace ECS_Engine
{
    class Animator : Component
    {
        public float MoveSpeed { get; set; } = 300f;
        public float SpinSpeed { get; set; } = 10f;
        public int FadeSpeed { get; set; } = 300;
        public Vector2 TargetPosition { get; private set; }
        public bool IsMoving { get; private set; } = false;
        public bool IsRotating { get; private set; } = false;
        public bool IsFading { get; private set; } = false;

        public void ToggleMoving()
        {
            IsMoving = !IsMoving;
        }

        public void ToggleRotation()
        {
            IsRotating = !IsRotating;
        }

        public void ToggleFading()
        {
            IsFading = !IsFading;
        }

        public void SetTargetPosition(Vector2 target)
        {
            TargetPosition = target;
        }
    }
}
