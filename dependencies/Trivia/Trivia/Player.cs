namespace LevelUp.Dependencies.Trivia
{
    internal sealed class Player
    {
        private readonly IOutput output;
        private readonly string name;
        private int score;

        public int Location { get; private set; }

        public Player(IOutput output, string name)
        {
            this.output = output;
            this.name = name;
            Location = 0;
            score = 0;
        }

        public void Move(int roll)
        {
            Location = (Location + roll) % 12;
            output.Write($"{name}'s new location is {Location}");
        }

        public void ScorePoint() => output.Write($"{name} now has {++score} Gold Coins.");

        public bool HasWon() => score == 6;

        public override string ToString() => name;
    }
}
