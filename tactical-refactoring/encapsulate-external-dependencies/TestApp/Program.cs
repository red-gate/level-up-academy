using System;
using System.IO;
using TestApp.Engine;

namespace TestApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Run(args, Console.Out, Console.Error);
        }

        public static void Run(string[] args, TextWriter stdout, TextWriter stderr)
        {
            Log.Initialize(stdout);
            stdout.WriteLine("Hello, world!");
        }
    }
}
