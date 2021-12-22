using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommandLineOptions
{
    internal class CommandLineParserConfigurationSource<TOptions> : IConfigurationSource where TOptions : class
    {
        private readonly string[] _args;
        private readonly IConfiguration _existingConfiguration;

        internal CommandLineParserConfigurationSource(string[] args, IConfiguration existingConfiguration)
        {
            _args = args;
            _existingConfiguration = existingConfiguration;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CommandLineParserConfigurationProvider<TOptions>(_args, _existingConfiguration);
        }
    }
}
