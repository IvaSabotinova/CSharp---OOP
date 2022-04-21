using System;


namespace SoftUniDI.Contacts
{
    public interface IModule
    {
        void Configure();
        //It accepts the interface we want to inject and the attribute its field or parameter has (the attribute can either be Inject or Named). The method returns the class which inherits the current interface. 
        Type GetMapping(Type currentInterface, object attribute);
        // which tries to get the instance of the current class. It returns the instance or null if that instance doesn’t exist. 
        object GetInstance(Type type);
        //accepts the current class and its instance as parameter
        void SetInstance(Type implementation, object instance);

    }
}
