using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocContainer
{
    public class Container
    {
        private readonly IDictionary<ObjToRegister, object> _registered = new Dictionary<ObjToRegister, object>();

        public bool Register<TType, TConcreteClass>()
        {
            return Register<TType, TConcreteClass>(LifestyleType.Transient); ;
        }

        public bool Register<TType, TConcreteClass>(int lifestyleType)
        {
            int currentCount = _registered.Count;
            var obj = new ObjToRegister(typeof(TType), typeof(TConcreteClass), lifestyleType);
            _registered.Add(obj, null);
            return (_registered.Count > currentCount);
        }

        public TConcreteClass Resolve<TConcreteClass>()
        {
            return (TConcreteClass)Resolve(typeof(TConcreteClass));
        }

        public object Resolve(Type concreteClass)
        {
            object ret = (from o in _registered.Keys where o.InterfaceType == concreteClass select GetInstance(o)).FirstOrDefault();

            //if ret is null, that means it was not registered.
            if (ret == null)
            {
                throw new NotRegisteredException("Register \"" + concreteClass + "\" before resolving.");
            }

            return ret;
        }

        private object GetInstance(ObjToRegister obj)
        {
            object ret = null;
            //create a new instance is there is no instance.
            //if the lifestyle type is transient, create a new instance everytime
            //if the lifestyle is singleton, return the same instance.
            if (obj.LifeStyleType == LifestyleType.Transient || _registered[obj] == null)
            {
                var consInfo = obj.ConcreteClassType.GetConstructors()[0];
                var consParams = consInfo.GetParameters();
                //if there are no params, just return a new instance.
                //if there are params, resolve the param types, add it to the constructor and return.
                if (consParams.Length < 1)
                {
                    ret = Activator.CreateInstance(obj.ConcreteClassType);
                }
                else
                {
                    ret = consInfo.Invoke(consParams.Select(p => Resolve(p.ParameterType)).ToArray());
                }
                _registered[obj] = ret;
            }
            else
            {
                ret = _registered[obj];
            }
            return ret;
        }
    }
}
