using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Gameboard : Component
    {
        public readonly int verticalElementsCount = 8;
        public readonly int horizontalElementsCount  = 8;
        public readonly int spacing = 128;

        public Gameboard()
        {

        }

        public Gameboard(int vCount, int hCount)
        {
            verticalElementsCount = vCount;
            horizontalElementsCount = hCount;
        }

        public Gameboard(int vCount, int hCount, int spacingValue)
        {
            verticalElementsCount = vCount;
            horizontalElementsCount = hCount;
            spacing = spacingValue;
        }
    }
}
