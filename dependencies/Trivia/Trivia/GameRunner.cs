using System;
using Ninject;

namespace LevelUp.Dependencies.Trivia
{
    internal static class GameRunner
    {
        private static bool isAWinner;

        public static void Main()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IOutput>().To<ConsoleWriter>();
            kernel.Bind<CategoriesSource>().ToSelf().WithConstructorArgument("categoryNames", new[] { "Pop", "Science", "Sports", "Rock" });
            kernel.Bind<PlayersSource>().ToSelf().WithConstructorArgument("playerNames", new[] { "Chet", "Pat", "Sue" });

            var aGame = kernel.Get<Game>();

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
