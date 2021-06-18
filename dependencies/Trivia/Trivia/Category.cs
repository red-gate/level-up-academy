namespace LevelUp.Dependencies.Trivia
{
    internal sealed class Category
    {
        private readonly string name;
        private int currentQuestion;

        public Category(string name) => this.name = name;

        public Question Next() => new(name, $"{name} Question {currentQuestion++}");
    }
}
