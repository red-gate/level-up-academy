namespace LevelUp.Dependencies.Calculator
{
    internal static class Program
    {
        internal static void Main()
        {
            var calculator = new Calculator(new ConsolePrinter());
            calculator.Add(1, 2);
        }
    }
}
