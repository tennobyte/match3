using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    public enum Gem: int
    {
        Ruby,
        Emerald,
        Diamond,
        Opal,
        Sapphire
    }

    class GameboardElement: Component
    {
        
        public Gem GemType { get; set; }
        public Point BoardPosition { get; set; } = new Point();

        public GameboardElement()
        {

        }

        public GameboardElement(Gem gemType)
        {
            GemType = gemType;
        }

        public GameboardElement(Point point)
        {
            BoardPosition = point;
        }

        public GameboardElement(Gem gemType, Point point)
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
