using System;

namespace LevelUp.Dependencies.Calculator
{
    internal sealed class ErrorHandlingPrinter : IOutputPrinter
    {
        private readonly IOutputPrinter _printer;

        public ErrorHandlingPrinter(IOutputPrinter printer) => _printer = printer;

        public void Print(int answer)
        {
            try
            {
                _printer.Print(answer);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error printing answer: {exception.Message}");
            }
        }
    }
}
