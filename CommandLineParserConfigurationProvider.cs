using CommandLine;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCommandLineOptions
{
    public class CommandLineParserConfigurationProvider<TOptions> : ConfigurationProvider where TOptions : class
    {
        private readonly string[] _args;
        private readonly IConfiguration _existingConfiguration;

        public CommandLineParserConfigurationProvider(string[] args, IConfiguration existingConfiguration)
        {
            _args = args;
            _existingConfiguration = existingConfiguration;
        }

        public override void Load()
        {
            PropertyInfo[] optionsProperties = typeof(TOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var optionsAttributes = optionsProperties
                .Select(p => (Property: p, Attribute: Attribute.GetCustomAttribute(p, typeof(OptionAttribute)) as OptionAttribute))
                .Where(a => a.Attribute is not null)
                .Cast<(PropertyInfo Property, OptionAttribute Attribute)>();

            IEnumerable<string> newArgs = optionsAttributes
                .Where(a => a.Attribute.Required 
                            && _existingConfiguration.GetChildren().Any(c => c.Key.ToLower() == a.Property.Name.ToLower())
                            && !_args.Contains($"-{a.Attribute.ShortName}")
                            && !_args.Contains($"--{a.Attribute.LongName}")
                            )
                .SelectMany(a => new string[] { $"--{a.Attribute.LongName}", $"{_existingConfiguration.GetValue<string>(a.Property.Name)}" });

            HashSet<string> keys = optionsAttributes.SelectMany(pa => new string[] { $"-{pa.Attribute.ShortName}", $"--{pa.Attribute.LongName}" }).ToHashSet();
            List<string> filteredArgs = new List<string>();
            for (int i = 0; i < _args.Length; i += 2)
            {
                if (keys.Contains(_args[i]))
                {
                    filteredArgs.AddRange(new string[] { _args[i], _args[i+1] });
                }
            }

            IEnumerable<string> args = filteredArgs.Concat(newArgs).ToArray();

            Parser.Default.ParseArguments<TOptions>(args)
                .WithParsed(o =>
                {
                    Data = optionsProperties.ToDictionary(p => p.Name, p => p.GetValue(o)?.ToString());
                })
                .WithNotParsed(_ => throw new ArgumentException("Either not all required parameters were specified or some were specified with a bad format. See error message above for additional details."));
        }
    }
}
