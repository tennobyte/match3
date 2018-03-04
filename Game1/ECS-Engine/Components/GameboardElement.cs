using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    public enum Gems: int
    {
        Ruby,
        Emerald,
        Diamond,
        Opal,
        Sapphire
    }

    class GameboardElement: Component
    {
        public bool IsClicked { get; set; } = false;
        public Gems GemType { get; set; }
        public Point BoardPosition { get; set; } = new Point();

        public GameboardElement()
        {

        }

        public GameboardElement(Gems gemType)
        {
            GemType = gemType;
        }

        public GameboardElement(Point point)
        {
            BoardPosition = point;
        }

        public GameboardElement(Gems gemType, Point point)
        {
            GemType = gemType;
            BoardPosition = point;
        }

        public void MoveDownBy(int offset)
        {
            BoardPosition = new Point(BoardPosition.X, BoardPosition.Y + offset);
        }
    }
}
