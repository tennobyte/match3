using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Transform : Component
    {
        public Vector2 Position { get;  set; }
        public float Scale { get;  set; }
        public float Rotation { get;  set; }

        public Transform()
        {
            Position = new Vector2(0);
            Scale = 1;
            Rotation = 0;
        }

        public Transform(Vector2 position)
        {
            Position = position;
            Scale = 1;
            Rotation = 0;
        }

        public Transform(Vector2 position, float scale, float rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void Move(Vector2 vector)
        {
            Position = Position + vector;
        }

        public void Rotate(float value)
        {
            Rotation += value;
        }

        public void SetRotation(float value)
        {
            Rotation = value;
        }

        public void SetScale(float value)
        {
            Scale = value;
        }
    }
}
