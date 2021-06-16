using System.Collections.Generic;

namespace LevelUp.Dependencies.Trivia
{
    internal sealed class Players
    {
        private readonly IOutput output;
        private readonly List<Player> players = new();
        private int currentPlayerNumber;

        public Players(IOutput output) => this.output = output;

        public void Add(string name)
        {
            players.Add(new Player(output, name));
            var playerNumber = players.Count;
            output.Write($"{name} was added");
            output.Write($"They are player number {playerNumber}");
        }

        public Player Current() => players[currentPlayerNumber];

        public void MoveToNextPlayer() => currentPlayerNumber = (currentPlayerNumber + 1) % players.Count;
    }
}
