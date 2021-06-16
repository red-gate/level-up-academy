using System.Collections.Generic;
using System.Linq;

namespace LevelUp.Dependencies.Trivia
{
    internal sealed class CategoriesSource
    {
        private readonly string[] categoryNames;

        public CategoriesSource(params string[] categoryNames) => this.categoryNames = categoryNames;

        public IEnumerable<Category> GetCategories() => categoryNames.Select(category => new Category(category));
    }
}
