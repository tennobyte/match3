using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Score : Component
    {
        public int ScoreValue { get; protected set; } = 0;

        public void IncreaseScore(int value)
        {
            ScoreValue += value;
        }

        public void SetScore(int value)
        {
            ScoreValue = value;
        }

    }
}
