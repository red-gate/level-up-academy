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
            Log.Initialize(stderr);

            var listCommand = new Command("list");
            listCommand.Handler = CommandHandler.Create<ListArgs>(List);

            var addCommand = new Command("add")
            {
                new Argument<string>("item"),
                new Option<string?>("--after", () => null),
                new Option<string?>("--before", () => null),
            };
            addCommand.Handler = CommandHandler.Create<AddArgs>(Add);

            var rootCommand = new RootCommand
            {
                listCommand,
                addCommand
            };
            rootCommand.AddGlobalOption(new Option<string>("--store"));
            return rootCommand.Invoke(args, new ConsoleWrapper(stdout, stderr));
        }

        private static async Task List(ListArgs args)
        {
            var list = new ToDoList(new JsonToDoStore(args.Store));
            foreach (var item in await list.GetItemsAsync())
            {
                args.Console.Out.WriteLine($"{(item.Complete ? '☑' : '☐')} {item.Item}");
            }
        }

        private static async Task Add(AddArgs args)
        {
            var list = new ToDoList(new JsonToDoStore(args.Store));

            if (args.After == null && args.Before == null)
            {
                await list.AddItemAsync(new ToDoItem(false, args.Item));
            }

            var position = 0;
            foreach (var existingItem in await list.GetItemsAsync())
            {
                if (existingItem.Item == args.After)
                {
                    await list.AddItemAsync(new ToDoItem(false, args.Item), position + 1);
                    return;
                }

                if (existingItem.Item == args.Before)
                {
                    await list.AddItemAsync(new ToDoItem(false, args.Item), position);
                    return;
                }

                position++;
            }

            args.Console.Error.WriteLine($"No item '{args.After ?? args.Before}' found");
        }
    }
}
