using System;

namespace LevelUp.Dependencies.Calculator
{
    internal sealed class Calculator
    {
        public void Add(int a, int b)
        {
            var sum = a + b;
            Console.WriteLine(sum);
        }
    }
}
