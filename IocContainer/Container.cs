using System;
using System.Collections.Generic;
using System.Linq;

namespace IocContainer
{
    public class Container
    {
        private readonly HashSet<ObjToRegister> _registeredObjects = new HashSet<ObjToRegister>();

        public bool Register<TType, TConcreteClass>()
        {
            return Register<TType, TConcreteClass>(LifestyleType.Transient);
        }

        public bool Register<TType, TConcreteClass>(LifestyleType lifestyleType)
        {
            var currentCount = _registeredObjects.Count;
            var obj = new ObjToRegister(typeof(TType), typeof(TConcreteClass), lifestyleType);
            _registeredObjects.Add(obj);
            return (_registeredObjects.Count > currentCount);
        }

        public TConcreteClass Resolve<TConcreteClass>()
        {
            return (TConcreteClass)Resolve(typeof(TConcreteClass));
        }

        public object Resolve(Type concreteClass)
        {
            object ret = (from o in _registeredObjects where o.InterfaceType == concreteClass select GetInstance(o)).FirstOrDefault();

            if (ret == null)
            {
                throw new NotRegisteredException("Register \"" + concreteClass + "\" before resolving.");
            }

            return ret;
        }

        private IEnumerable<object> ResolveConstructorParamaters(ObjToRegister objToRegister)
        {
            var constructorInfo = objToRegister.ConcreteClassType.GetConstructors().First();
            return constructorInfo.GetParameters().Select(parameter => Resolve(parameter.ParameterType));
        }

        private object GetInstance(ObjToRegister obj)
        {
            if(obj.Instance == null || obj.LifeStyleType == LifestyleType.Transient)
            {
                var paramaters = ResolveConstructorParamaters(obj);
                obj.CreateInstance((paramaters.ToArray()));
            }
            return obj.Instance;
        }
    }
}
