using System;
using CommandLine;

namespace NetCommandLineOptions
{
    public class Options
    {
        [Option('t', "test", Required = true, HelpText = "a test required option")]
        public string Test { get; set; }

        [Option('i', "my-int", Required = true, HelpText = "a test non-required int")]
        public int MyInt { get; set; }

        [Option('d', "my-date", Required = true, HelpText = "a test non-required date")]
        public DateTime MyDate { get; set; }
    }
}