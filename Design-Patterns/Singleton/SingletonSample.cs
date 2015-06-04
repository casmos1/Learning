using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns.Singleton
{
    internal class SingletonSample
    {
        private SingletonSample()
        {
        }

        private static volatile SingletonSample singletonObject;
        private static object lockingObject = new object();

        public static SingletonSample InstanceCreation()
        {
            if (singletonObject == null)
            {
                lock (lockingObject)
                {
                    if (singletonObject == null)
                    {
                        singletonObject = new SingletonSample();
                    }
                }
            }
            
            return singletonObject;
        }

        public void DisplayMessage()
        {
            Console.WriteLine("My First Singleton Program");
        }
    }
}
