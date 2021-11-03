using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.IO;
using System.Threading.Tasks;
using TestApp.Engine;

namespace TestApp
{
    public static class Program
    {
        private static int Main(string[] args)
        {
            return Run(args, Console.Out, Console.Error);
        }

        public static int Run(string[] args, TextWriter stdout, TextWriter stderr)
        {
            Log.Initialize(stdout);
            var rootCommand = new RootCommand
            {
                new Command("list")
                {
                    Handler = CommandHandler.Create<string, IConsole>(List)
                }
            };
            rootCommand.AddGlobalOption(new Option<string>("--store"));
            return rootCommand.Invoke(args, new ConsoleWrapper(stdout, stderr));
        }

        private static async Task List(string store, IConsole console)
        {
            var list = new ToDoList(new JsonToDoStore(store));
            foreach (var item in await list.GetItemsAsync())
            {
                console.Out.WriteLine($"{(item.Complete ? '☑' : '☐')} {item.Item}");
            }
        }
    }
}
