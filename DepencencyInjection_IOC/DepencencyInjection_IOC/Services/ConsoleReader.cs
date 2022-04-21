using System;
using System.Collections.Generic;
using System.Text;
using DependencyInjection_IOC.Contracts;

namespace DependencyInjection_IOC.Services
{
  public  class ConsoleReader: IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
