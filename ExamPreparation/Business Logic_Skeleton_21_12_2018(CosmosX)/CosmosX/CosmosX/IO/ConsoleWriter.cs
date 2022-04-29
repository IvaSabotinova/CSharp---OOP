using CosmosX.IO.Contracts;
using System;
using System.IO;

namespace CosmosX.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string output)
        {
            File.AppendAllText("sekretenDokuemntNeGoPipai.txt", output+ Environment.NewLine);
            
            Console.WriteLine(output);

        }

    }
}