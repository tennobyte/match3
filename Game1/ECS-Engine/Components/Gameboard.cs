namespace ECS_Engine
{
    class Gameboard : Component
    {
        public int VerticalElementsCount { get; private set; } = 8;
        public int HorizontalElementsCount { get; private set; } = 8;
        public int Spacing { get; private set; } = 128;

        public Gameboard()
        {

        }

        public Gameboard(int vCount, int hCount)
        {
            VerticalElementsCount = vCount;
            HorizontalElementsCount = hCount;
        }

        public Gameboard(int vCount, int hCount, int spacingValue)
        {
            VerticalElementsCount = vCount;
            HorizontalElementsCount = hCount;
            Spacing = spacingValue;
        }
    }
}
