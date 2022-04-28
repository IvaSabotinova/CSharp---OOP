using HAD.Contracts;
using System;
using System.IO;
using System.Text;

namespace HAD.IO
{
    public class ConsoleWriter : IWriter
    {
        private readonly StringBuilder sb;

        public ConsoleWriter()
        {
            this.sb = new StringBuilder();
        }

        public void WriteLine(string text)
        {
            sb.AppendLine(text);

        }

        public void Flush()
        {
            Console.WriteLine(sb.ToString().Trim());
            File.AppendAllText("log.txt", sb.ToString().Trim());
        }
    }
}