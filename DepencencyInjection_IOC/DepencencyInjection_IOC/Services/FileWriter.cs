
using System.IO;

using DependencyInjection_IOC.Contracts;

namespace DependencyInjection_IOC.Services
{
    public class FileWriter: IFileWriter
    {
        public void Write(string text)
        {
            File.WriteAllText("log.txt", text);
        }
    }
}
