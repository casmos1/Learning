using System;

namespace Design_Patterns.Pattern_Observer
{
    public class Observer : IObserver
    {
        public string ObserverName { get; private set; }

        public Observer(string name)
        {
            ObserverName = name;
        }

        public void Update()
        {
            Console.WriteLine("{0}: A new product has arrived at the store", this.ObserverName);
        }
    }

    internal interface IObserver
    {
        void Update();
    }
}
