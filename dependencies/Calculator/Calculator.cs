namespace LevelUp.Dependencies.Calculator
{
    internal sealed class Calculator
    {
        private readonly IOutputPrinter _printer;

        public Calculator(IOutputPrinter printer) => _printer = printer;

        public void Add(int a, int b)
        {
            var sum = a + b;
            _printer.Print(sum);
        }
    }
}
