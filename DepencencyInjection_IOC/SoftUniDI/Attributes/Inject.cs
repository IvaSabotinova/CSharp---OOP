using System;

namespace SoftUniDI.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Field, AllowMultiple = true) ]
    public class Inject: Attribute
    {
    }
}
