using DependencyInjection_IOC.Contracts;
using System;

namespace DependencyInjection_IOC.Services
{
    public  class ConsoleWriter: IConsoleWriter
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
