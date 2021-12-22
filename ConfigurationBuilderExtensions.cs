using NetCommandLineOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddCommandLineParser<TOptions>(this IConfigurationBuilder builder, string[] args) where TOptions : class
        {
            IConfiguration tempConfig = builder.Build();
            return builder.Add(new CommandLineParserConfigurationSource<TOptions>(args, tempConfig));
        }
    }
}
