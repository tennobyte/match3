namespace ECS_Engine
{
    class Score : Component
    {
        public int ScoreValue { get; private set; } = 0;

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
