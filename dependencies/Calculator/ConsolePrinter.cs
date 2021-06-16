using System;

namespace LevelUp.Dependencies.Calculator
{
    internal sealed class ConsolePrinter : IOutputPrinter
    {
        public void Print(int answer)
        {
            throw new Exception("An error");
        }
    }
}
