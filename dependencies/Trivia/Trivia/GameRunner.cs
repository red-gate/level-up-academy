using System;

namespace LevelUp.Dependencies.Trivia
{
    internal static class GameRunner
    {
        private static bool isAWinner;

        public static void Main()
        {
            var output = new ConsoleWriter();

            var board = new Board(output, "Pop", "Science", "Sports", "Rock");

            var players = new Players(output);
            players.Add("Chet");
            players.Add("Pat");
            players.Add("Sue");

            var penaltyBox = new PenaltyBox(output);

            var aGame = new Game(output, board, players, penaltyBox);

            var rand = new Random(1337);

            do
            {
                aGame.Roll(rand.Next(5) + 1);
                isAWinner = rand.Next(9) == 7
                    ? aGame.AnsweredIncorrectly()
                    : aGame.AnsweredCorrectly();
            }
            while (!isAWinner);
        }
    }
}
