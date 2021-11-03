using System;
using System.CommandLine;
using System.CommandLine.IO;
using System.IO;

namespace TestApp
{
    public sealed class ConsoleWrapper : IConsole
    {
        internal ConsoleWrapper(TextWriter stdout, TextWriter stderr)
        {
            Out = StandardStreamWriter.Create(stdout);
            IsOutputRedirected = stdout != Console.Out || Console.IsOutputRedirected;
            Error = StandardStreamWriter.Create(stderr);
            IsErrorRedirected = stderr != Console.Error || Console.IsErrorRedirected;
            IsInputRedirected = Console.IsInputRedirected;
        }

        public IStandardStreamWriter Out { get; }
        public bool IsOutputRedirected { get; }
        public IStandardStreamWriter Error { get; }
        public bool IsErrorRedirected { get; }
        public bool IsInputRedirected { get; }
    }
}
