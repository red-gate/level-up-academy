namespace LevelUp.Dependencies.Trivia
{
    internal sealed class Game
    {
        private readonly IOutput output;
        private readonly Players players;
        private readonly Board board;
        private readonly PenaltyBox penaltyBox;

        public Game(IOutput output, Board board, Players players, PenaltyBox penaltyBox)
        {
            this.output = output;
            this.board = board;
            this.players = players;
            this.penaltyBox = penaltyBox;
        }

        public void Roll(int roll)
        {
            var currentPlayer = players.Current();
            output.Write($"{currentPlayer} is the current player");
            output.Write($"They have rolled a {roll}");

            if (penaltyBox.ApplyRoll(currentPlayer, roll))
            {
                currentPlayer.Move(roll);
                board.AskNextQuestion(currentPlayer.Location);
            }
        }

        public bool AnsweredCorrectly()
        {
            output.Write("Answer was correct!!!!");

            var currentPlayer = players.Current();
            currentPlayer.ScorePoint();

            players.MoveToNextPlayer();
            return currentPlayer.HasWon();
        }

        public bool AnsweredIncorrectly()
        {
            output.Write("Question was incorrectly answered");

            penaltyBox.Add(players.Current());
            players.MoveToNextPlayer();
            return false;
        }
    }
}
