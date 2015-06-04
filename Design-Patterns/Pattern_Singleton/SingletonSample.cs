using System;

namespace Design_Patterns.Pattern_Singleton
{
    internal class SingletonSample
    {
        private SingletonSample()
        {
        }

        private static volatile SingletonSample singletonObject;
        private static readonly object lockingObject = new object();

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
