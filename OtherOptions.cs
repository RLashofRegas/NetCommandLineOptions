using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommandLineOptions
{
    public class OtherOptions
    {

        [Option('o', "other-option", Required = true, HelpText = "a test required other option")]
        public string MyOtherOption { get; set; }
    }
}
