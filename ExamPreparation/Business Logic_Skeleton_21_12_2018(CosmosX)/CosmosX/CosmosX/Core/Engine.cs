using CosmosX.Core.Contracts;
using CosmosX.IO.Contracts;
using System;

namespace CosmosX.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private ICommandParser commandParser;
        private bool isRunning;

        public Engine(IReader reader, IWriter writer, ICommandParser commandParser)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandParser = commandParser;
            this.isRunning = true;
        }

        public void Run()
        {

            while (isRunning)
            {
                string line = reader.ReadLine();
                string[] input = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string result = null;
                try
                {
                    if (line == "Exit")
                    {
                        result = commandParser.Parse(input);
                        writer.WriteLine(result);
                        isRunning = false;

                    }
                    else
                    {
                        result = commandParser.Parse(input);
                        writer.WriteLine(result);

                    }
                }
                catch (Exception e)
                {
                    writer.WriteLine(e.Message);
                }

            }
        }
    }
}