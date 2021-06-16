using System.Collections.Generic;
using System.Linq;

namespace LevelUp.Dependencies.Trivia
{
    internal sealed class Board
    {
        private readonly IOutput output;
        private readonly List<Category> categories;

        public Board(IOutput output, params string[] categoryNames)
        {
            this.output = output;
            categories = categoryNames.Select(category => new Category(category)).ToList();
        }

        public void AskNextQuestion(int currentPosition)
        {
            var category = categories[currentPosition % categories.Count];
            var question = category.Next();
            question.Write(output);
        }
    }
}
