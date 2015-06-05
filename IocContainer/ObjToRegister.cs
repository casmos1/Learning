using System;

namespace IocContainer
{
    //class to register stuff. holds the interface, the class as well as the lifestyle type
    //this way, I can create one dictionary to hold everything instead of dictionary of dictionaries.
    class ObjToRegister
    {
        public ObjToRegister(Type interfaceType, Type concreteClassType, int lifestyle)
        {
            InterfaceType = interfaceType;
            ConcreteClassType = concreteClassType;
            LifeStyleType = lifestyle;
        }

        public Type InterfaceType{ get; set; }
        public Type ConcreteClassType { get; set; }
        public int LifeStyleType { get; set; }
    }
}
