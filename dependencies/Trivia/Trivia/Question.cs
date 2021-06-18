namespace LevelUp.Dependencies.Trivia
{
    internal sealed class Question
    {
        private readonly string category;
        private readonly string content;

        public Question(string category, string content)
        {
            this.category = category;
            this.content = content;
        }

        public void Write(IOutput output)
        {
            output.Write($"The category is {category}");
            output.Write(content);
        }
    }
}
