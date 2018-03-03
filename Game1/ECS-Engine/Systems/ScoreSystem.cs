using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class ScoreSystem : System
    {
        public ScoreSystem()
            : base(typeof(Transform), typeof(TextRenderer))
        {

        }
    }
}
