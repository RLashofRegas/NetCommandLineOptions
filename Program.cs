using CommandLine;
using NetCommandLineOptions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddCommandLineParser<Options>(args)
    .AddCommandLineParser<OtherOptions>(args)
    .Build();

Options options = config.Get<Options>();

Console.WriteLine($"Test = '{options.Test}'");
Console.WriteLine($"MyInt = '{options.MyInt}'");
Console.WriteLine($"MyDate = '{options.MyDate}'");

OtherOptions otherOptions = config.Get<OtherOptions>();

Console.WriteLine($"OtherOption = '{otherOptions.MyOtherOption}'");