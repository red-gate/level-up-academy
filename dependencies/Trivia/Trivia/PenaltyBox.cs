using System.Collections.Generic;

namespace LevelUp.Dependencies.Trivia
{
    internal sealed class PenaltyBox
    {
        private readonly IOutput output;
        private readonly List<Player> players = new();

        public PenaltyBox(IOutput output) => this.output = output;

        public void Add(Player player)
        {
            output.Write($"{player} was sent to the penalty box");
            players.Add(player);
        }

        public bool ApplyRoll(Player player, int roll)
        {
            if (!players.Contains(player))
            {
                return true;
            }

            if (roll % 2 == 0)
            {
                output.Write($"{player} is not getting out of the penalty box");
                return false;
            }

            output.Write($"{player} is getting out of the penalty box");
            players.Remove(player);
            return true;
        }
    }
}
