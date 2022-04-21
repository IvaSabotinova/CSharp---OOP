
using DependencyInjection_IOC.Contracts;


namespace DependencyInjection_IOC.Core
{
    public class Engine
    {
        //     [Inject]
        //     [Named("ConsoleWriter")]
        //     private IWriter consoleWriter;

        //     [Inject]
        //     [Named("FileWriter")]
        //     private IWriter fileWriter;
        //     [Inject]
        //     private IReader reader;

        private readonly IWriter consoleWriter;
        private readonly IWriter fileWriter;
        private readonly IReader reader;

        public Engine(IConsoleWriter _consoleWriter, IFileWriter _fileWriter, IReader _reader)
        {
            consoleWriter = _consoleWriter;
            fileWriter = _fileWriter;
            reader = _reader;
        }
        //[Inject]
        //public Engine([Named("ConsoleWriter")] IWriter _consoleWriter, [Named("FileWriter")] IWriter _fileWriter, IReader _reader)
        //{
        //    consoleWriter = _consoleWriter;
        //    fileWriter = _fileWriter;
        //    reader = _reader;
        //}

        public void Run()
        {
            string text = reader.Read();
            fileWriter.Write(text);
            consoleWriter.Write(text);
        }

    }
}
