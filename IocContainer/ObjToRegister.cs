using System;

namespace IocContainer
{
    class ObjToRegister
    {
        public ObjToRegister(Type interfaceType, Type concreteClassType, LifestyleType lifestyle)
        {
            InterfaceType = interfaceType;
            ConcreteClassType = concreteClassType;
            LifeStyleType = lifestyle;
        }

        public Type InterfaceType{ get; set; }
        public Type ConcreteClassType { get; set; }
        public LifestyleType LifeStyleType { get; set; }
        public object Instance { get; private set; }

        public void CreateInstance(params object[] args)
        {
            Instance = Activator.CreateInstance(ConcreteClassType, args);
        }
    }
}
