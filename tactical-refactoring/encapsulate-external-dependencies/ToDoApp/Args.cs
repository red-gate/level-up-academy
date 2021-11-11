using System.CommandLine;

namespace ToDoApp
{
#pragma warning disable 8618
    internal abstract class CommonArgs
    {
        public string Store { get; set; }
        public IConsole Console { get; set; }
    }

    internal sealed class ListArgs : CommonArgs
    {
    }

    internal sealed class AddArgs : CommonArgs
    {
        public string Item { get; set; }
        public string? After { get; set; }
        public string? Before { get; set; }
    }

    internal sealed class ItemArgs : CommonArgs
    {
        public string Item { get; set; }
    }
#pragma warning restore 8618
}
