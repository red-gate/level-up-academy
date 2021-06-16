using System.Collections.Generic;
using System.Linq;

namespace LevelUp.Dependencies.Trivia
{
    internal sealed class PlayersSource
    {
        private readonly IOutput output;
        private readonly string[] playerNames;

        public PlayersSource(IOutput output, params string[] playerNames)
        {
            this.output = output;
            this.playerNames = playerNames;
        }

        public IEnumerable<Player> GetPlayers() => playerNames.Select(name => new Player(output, name));
    }
}
