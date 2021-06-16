using System;
using System.Text;
using NUnit.Framework;

namespace LevelUp.Dependencies.Trivia.Tests
{
    [TestFixture]
    public sealed class GoldenMaster
    {
        [Test]
        public void ConsoleOutputIsCorrect()
        {
            var output = new TestOutputWriter();

            var board = new Board(output, new CategoriesSource("Pop", "Science", "Sports", "Rock"));

            var players = new Players(output, new PlayersSource(output, "Chet", "Pat", "Sue"));

            var penaltyBox = new PenaltyBox(output);

            var game = new Game(output, board, players, penaltyBox);

            var rand = new Random(1337);
            bool isAWinner;

            do
            {
                game.Roll(rand.Next(5) + 1);
                isAWinner = rand.Next(9) == 7
                    ? game.AnsweredIncorrectly()
                    : game.AnsweredCorrectly();
            }
            while (!isAWinner);

            Assert.That(output.Text, Is.EqualTo(ExpectedString));
        }

        private sealed class TestOutputWriter : IOutput
        {
            private readonly StringBuilder output = new();

            public string Text => output.ToString();

            public void Write(string text) => output.AppendLine(text);
        }


        private const string ExpectedString = @"Chet was added
They are player number 1
Pat was added
They are player number 2
Sue was added
They are player number 3
Chet is the current player
They have rolled a 2
Chet's new location is 2
The category is Sports
Sports Question 0
Answer was correct!!!!
Chet now has 1 Gold Coins.
Pat is the current player
They have rolled a 2
Pat's new location is 2
The category is Sports
Sports Question 1
Answer was correct!!!!
Pat now has 1 Gold Coins.
Sue is the current player
They have rolled a 5
Sue's new location is 5
The category is Science
Science Question 0
Answer was correct!!!!
Sue now has 1 Gold Coins.
Chet is the current player
They have rolled a 5
Chet's new location is 7
The category is Rock
Rock Question 0
Answer was correct!!!!
Chet now has 2 Gold Coins.
Pat is the current player
They have rolled a 2
Pat's new location is 4
The category is Pop
Pop Question 0
Answer was correct!!!!
Pat now has 2 Gold Coins.
Sue is the current player
They have rolled a 2
Sue's new location is 7
The category is Rock
Rock Question 1
Answer was correct!!!!
Sue now has 2 Gold Coins.
Chet is the current player
They have rolled a 4
Chet's new location is 11
The category is Rock
Rock Question 2
Answer was correct!!!!
Chet now has 3 Gold Coins.
Pat is the current player
They have rolled a 5
Pat's new location is 9
The category is Science
Science Question 1
Answer was correct!!!!
Pat now has 3 Gold Coins.
Sue is the current player
They have rolled a 5
Sue's new location is 0
The category is Pop
Pop Question 1
Answer was correct!!!!
Sue now has 3 Gold Coins.
Chet is the current player
They have rolled a 5
Chet's new location is 4
The category is Pop
Pop Question 2
Answer was correct!!!!
Chet now has 4 Gold Coins.
Pat is the current player
They have rolled a 3
Pat's new location is 0
The category is Pop
Pop Question 3
Question was incorrectly answered
Pat was sent to the penalty box
Sue is the current player
They have rolled a 5
Sue's new location is 5
The category is Science
Science Question 2
Answer was correct!!!!
Sue now has 4 Gold Coins.
Chet is the current player
They have rolled a 4
Chet's new location is 8
The category is Pop
Pop Question 4
Answer was correct!!!!
Chet now has 5 Gold Coins.
Pat is the current player
They have rolled a 3
Pat is getting out of the penalty box
Pat's new location is 3
The category is Rock
Rock Question 3
Answer was correct!!!!
Pat now has 4 Gold Coins.
Sue is the current player
They have rolled a 5
Sue's new location is 10
The category is Sports
Sports Question 2
Question was incorrectly answered
Sue was sent to the penalty box
Chet is the current player
They have rolled a 4
Chet's new location is 0
The category is Pop
Pop Question 5
Answer was correct!!!!
Chet now has 6 Gold Coins.
";
    }
}
