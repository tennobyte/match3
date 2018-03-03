using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Animator : Component
    {
        public float MoveSpeed { get; set; }
        public float SpinSpeed { get; set; }
        public Vector2 TargetPosition { get; private set; }
        public bool IsMoving { get; private set; }
        public bool IsSpinning { get; private set; }

        public void EnableMoving(bool isEnabled)
        {
            IsMoving = isEnabled;
        }

        public void EnableSpinning(bool isEnabled)
        {
            IsSpinning = isEnabled;
        }

        public void SetTargetPosition(Vector2 target)
        {

        }
    }
}
