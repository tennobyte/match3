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

    }
}
