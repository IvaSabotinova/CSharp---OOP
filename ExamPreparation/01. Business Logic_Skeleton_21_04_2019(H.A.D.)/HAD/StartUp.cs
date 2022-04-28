using HAD.Contracts;
using HAD.Core;
using HAD.IO;

namespace HAD
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ConsoleReader consoleReader = new ConsoleReader();
            ConsoleWriter consoleWriter = new ConsoleWriter();
            IManager manager = new HeroManager();
            CommandProcessor commandProcessor = new CommandProcessor(manager);
            Engine engine = new Engine(consoleReader, consoleWriter, commandProcessor);

            //IServiceCollection serviceCollection = new ServiceCollection();
            //serviceCollection.AddTransient<IReader, ConsoleReader>().AddTransient<IWriter, ConsoleWriter>().AddTransient<ICommandProcessor, CommandProcessor>().AddTransient<IManager, HeroManager>().AddTransient<Engine>();
            //Engine engine = serviceCollection.BuildServiceProvider().GetService<Engine>();


            engine.Run();
        }
    }
}
