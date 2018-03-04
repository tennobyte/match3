using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Timer : Component
    {
        public float MaxTime { get; protected set; }
        public float TimeLeft { get; protected set; }

        public Timer()
        {

        }

        public Timer(float timeToSet)
        {
            MaxTime = timeToSet;
            TimeLeft = timeToSet;
        }

        public int GetSecondsLeft()
        {
            return (int)Math.Round(TimeLeft);
        }

        public void DecreaseTime(float value)
        {
            TimeLeft -= value;
        }

        public bool IsOutOfTime()
        {
            return TimeLeft <= 0;
        }
    }
}
