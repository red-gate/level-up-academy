using Ninject.Modules;

namespace LevelUp.Dependencies.Calculator
{
    internal sealed class PrinterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOutputPrinter>().To<ConsolePrinter>().WhenInjectedInto<ErrorHandlingPrinter>();
            Bind<IOutputPrinter>().To<ErrorHandlingPrinter>();
        }
    }
}
