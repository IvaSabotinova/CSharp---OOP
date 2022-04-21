using System;
using DependencyInjection_IOC.Contracts;
using DependencyInjection_IOC.Core;
using DependencyInjection_IOC.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection_IOC.Infrastructure
{
    public static class DependencyResolver
    {
        public static IServiceProvider GetServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IReader, ConsoleReader>().AddTransient<IConsoleWriter, ConsoleWriter>()
                .AddTransient<IFileWriter, FileWriter>().AddTransient<Engine>();
            return serviceCollection.BuildServiceProvider();
        }

    }
}
