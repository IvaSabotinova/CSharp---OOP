using DependencyInjection_IOC.Core;
using DependencyInjection_IOC.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace DependencyInjection_IOC
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = DependencyResolver.GetServiceProvider();
            //var engine = (Engine) serviceProvider.GetService(typeof(Engine));
            var engine = serviceProvider.GetService<Engine>();
            //ConsoleWriter consoleWriter = new ConsoleWriter();
            //var fileWriter = new FileWriter();
            //var reader = new ConsoleReader();
            //var engine = new Engine(consoleWriter, fileWriter, reader);

            //var injector = DependencyInjector.CreateInjector(new Module());
            //var engine = injector.Inject<Engine>();

            engine.Run();
        }
    }
}
