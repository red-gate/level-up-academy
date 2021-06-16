using System.Collections.Generic;

namespace LevelUp.Dependencies.Trivia
{
    internal sealed class Players
    {
        private readonly List<Player> players = new();
        private int currentPlayerNumber;

        public Players(IOutput output, PlayersSource playersSource)
        {
            foreach (var player in playersSource.GetPlayers())
            {
                players.Add(player);
                var playerNumber = players.Count;
                output.Write($"{player} was added");
                output.Write($"They are player number {playerNumber}");
            }
        }

        public Player Current() => players[currentPlayerNumber];

        public void MoveToNextPlayer() => currentPlayerNumber = (currentPlayerNumber + 1) % players.Count;
    }
}
