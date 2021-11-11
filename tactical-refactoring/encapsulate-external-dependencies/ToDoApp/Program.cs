using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Engine;

namespace ToDoApp
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
                new Option<string?>("--before", () => null)
            };
            addCommand.Handler = CommandHandler.Create<AddArgs>(Add);

            var completeCommand = new Command("complete")
            {
                new Argument<string>("item")
            };
            completeCommand.Handler = CommandHandler.Create<ItemArgs>(Complete);

            var uncompleteCommand = new Command("uncomplete")
            {
                new Argument<string>("item")
            };
            uncompleteCommand.Handler = CommandHandler.Create<ItemArgs>(Uncomplete);

            var removeCommand = new Command("remove")
            {
                new Argument<string>("item")
            };
            removeCommand.Handler = CommandHandler.Create<ItemArgs>(Remove);

            var rootCommand = new RootCommand
            {
                listCommand,
                addCommand,
                completeCommand,
                uncompleteCommand,
                removeCommand
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

        private static async Task<int> Add(AddArgs args)
        {
            var list = new ToDoList(new JsonToDoStore(args.Store));

            if (args.After == null && args.Before == null)
            {
                await list.AddItemAsync(new ToDoItem(false, args.Item));
                return 0;
            }

            var position = 0;
            foreach (var existingItem in await list.GetItemsAsync())
            {
                if (existingItem.Item == args.After)
                {
                    await list.AddItemAsync(new ToDoItem(false, args.Item), position + 1);
                    return 0;
                }

                if (existingItem.Item == args.Before)
                {
                    await list.AddItemAsync(new ToDoItem(false, args.Item), position);
                    return 0;
                }

                position++;
            }

            args.Console.Error.WriteLine($"No item '{args.After ?? args.Before}' found");
            return 1;
        }

        private static async Task<int> Complete(ItemArgs args)
        {
            var list = new ToDoList(new JsonToDoStore(args.Store));
            var item = (await list.GetItemsAsync()).FirstOrDefault(x => x.Item == args.Item);
            if (item == null)
            {
                args.Console.Error.WriteLine($"No item '{args.Item}' found");
                return 1;
            }
            else
            {
                await list.CompleteItemAsync(item);
                return 0;
            }
        }

        private static async Task<int> Uncomplete(ItemArgs args)
        {
            var list = new ToDoList(new JsonToDoStore(args.Store));
            var item = (await list.GetItemsAsync()).FirstOrDefault(x => x.Item == args.Item);
            if (item == null)
            {
                args.Console.Error.WriteLine($"No item '{args.Item}' found");
                return 1;
            }
            else
            {
                await list.UncompleteItemAsync(item);
                return 0;
            }
        }

        private static async Task<int> Remove(ItemArgs args)
        {
            var list = new ToDoList(new JsonToDoStore(args.Store));
            var item = (await list.GetItemsAsync()).FirstOrDefault(x => x.Item == args.Item);
            if (item == null)
            {
                args.Console.Error.WriteLine($"No item '{args.Item}' found");
                return 1;
            }
            else
            {
                await list.RemoveItemAsync(item);
                return 0;
            }
        }
    }
}
