using System;
using NUnit.Framework;
using Design_Patterns.Pattern_Singleton;

namespace Design_Patterns.Test
{
    public class Singleton
    {
        [Test]
        public void check_if_object_persists()
        {
            SingletonSample singleton = SingletonSample.InstanceCreation();
            singleton.DisplayMessage();
            Console.ReadLine();
        }
    }
}
