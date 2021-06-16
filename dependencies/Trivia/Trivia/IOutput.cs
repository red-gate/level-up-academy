using System;

namespace LevelUp.Dependencies.Trivia
{
    internal interface IOutput
    {
        void Write(string text);
    }

    internal sealed class ConsoleWriter : IOutput
    {
        public void Write(string text) => Console.WriteLine(text);
    }
}
