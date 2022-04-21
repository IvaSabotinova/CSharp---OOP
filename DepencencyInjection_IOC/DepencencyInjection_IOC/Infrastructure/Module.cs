using DependencyInjection_IOC.Contracts;
using DependencyInjection_IOC.Services;
using SoftUniDI.Modules;

namespace DependencyInjection_IOC.Infrastructure
{
    public class Module: AbstractModule
    {
        public override void Configure()
        {
            CreateMapping<IReader, ConsoleReader>();
            CreateMapping<IWriter, ConsoleWriter>();
            CreateMapping<IWriter, FileWriter>();
        }
    }
}
