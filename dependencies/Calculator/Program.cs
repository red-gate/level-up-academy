using Ninject;

namespace LevelUp.Dependencies.Calculator
{
    internal static class Program
    {
        internal static void Main()
        {
            var kernel = new StandardKernel();
            kernel.Load<PrinterModule>();

            var calculator = kernel.Get<Calculator>();
            calculator.Add(1, 2);
        }
    }
}
