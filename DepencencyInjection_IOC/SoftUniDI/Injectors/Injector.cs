using SoftUniDI.Attributes;
using SoftUniDI.Contacts;
using System;
using System.Linq;
using System.Reflection;

namespace SoftUniDI.Injectors
{
    public  class Injector
  {
      private IModule module;

      public Injector(IModule module)
      {
          this.module = module;
      }

      private bool CheckForFieldInjection<TClass>()
      {
          return typeof(TClass).GetFields((BindingFlags) 62)
              .Any(field => field.GetCustomAttributes(typeof(Inject), true).Any());
      }

      private bool CheckForConstructorInjection<TClass>()
      {
          return typeof(TClass).GetConstructors()
              .Any(constructor => constructor.GetCustomAttributes(typeof(Inject), true).Any());
      }
      private TClass  CreateConstructorInjection<TClass>()
      {
          Type desireClass = typeof(TClass);
          if (desireClass == null)
          {
              return default(TClass);
          }

          ConstructorInfo[] constructors = desireClass.GetConstructors();
          foreach (ConstructorInfo constructor in constructors)
          {
              if (!CheckForConstructorInjection<TClass>())
              {
                  continue;
              }

              Inject inject = (Inject) constructor.GetCustomAttributes(typeof(Inject), true).FirstOrDefault();
              ParameterInfo[] parameterTypes = constructor.GetParameters();
              object[] constructorParams = new object[parameterTypes.Length];
              var i = 0;
              foreach (ParameterInfo parameterType in parameterTypes)
              {
                  var named = parameterType.GetCustomAttributes(typeof(Named));
                  Type dependency = null;
                  if (named == null)
                  {
                      dependency = module.GetMapping(parameterType.ParameterType, inject);
                  }
                  else
                  {
                      dependency = module.GetMapping(parameterType.ParameterType, named);
                  }

                  if (parameterType.ParameterType.IsAssignableFrom(dependency))
                  {
                      object instance = module.GetInstance(dependency);
                      if (instance != null)
                      {
                          constructorParams[i++] = instance;
                      }
                      else
                      {
                          instance = Activator.CreateInstance(dependency);
                          constructorParams[i++] = instance;
                          module.SetInstance(parameterType.ParameterType, instance);
                      }
                  }
              }

              return (TClass) Activator.CreateInstance(desireClass, constructorParams);
          }

          return default(TClass);
      }

      private TClass CreateFieldsInjection<TClass>()
      {
          Type desireClass = typeof(TClass);
         object desireClassInstance = module.GetInstance(desireClass);
          if (desireClassInstance == null)
          {
              desireClassInstance = Activator.CreateInstance(desireClass);
              module.SetInstance(desireClass, desireClassInstance);
          }

          FieldInfo[] fields = desireClass.GetFields((BindingFlags) 62);
          foreach (FieldInfo field in fields)
          {
              if (field.GetCustomAttributes(typeof(Inject), true).Any())
              {
                  Inject injection = (Inject) field.GetCustomAttributes(typeof(Inject), true).FirstOrDefault();
                  Type dependency = null;

                  object[] named = field.GetCustomAttributes(typeof(Named), true);
                  var type = field.FieldType;
                  if (named == null)
                  {
                      dependency = module.GetMapping(type, injection);
                  }
                  else
                  {
                      dependency = module.GetMapping(type, named);
                  }

                  if (type.IsAssignableFrom(dependency))
                  {
                      object instance = module.GetInstance(dependency);
                      if (instance == null)
                      {
                          instance = Activator.CreateInstance(dependency);
                          module.SetInstance(dependency, instance);
                      }
                      field.SetValue(desireClassInstance, instance);
                  }
              }
          }

          return (TClass) desireClassInstance;

      }

      public TClass Inject<TClass>()
      {
          bool hasConstructorAttribute = CheckForConstructorInjection<TClass>();
          bool hasFieldAttribute = CheckForFieldInjection<TClass>();
          if (hasConstructorAttribute && hasFieldAttribute)
          {
              throw new ArgumentException("There must be only field or constructor annotated with Inject attribute");
          }

          if (hasConstructorAttribute)
          {
              return CreateConstructorInjection<TClass>();
          }
          else if (hasFieldAttribute)
          {
              return CreateFieldsInjection<TClass>();
          }

          return default(TClass);
      }
  }
}
