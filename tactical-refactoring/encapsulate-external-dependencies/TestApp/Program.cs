using System;
using System.IO;

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
            stdout.WriteLine("Hello, world!");
        }
    }
}
