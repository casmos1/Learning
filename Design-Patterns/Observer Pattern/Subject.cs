using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns.Observer_Pattern
{
    public class Subject : ISubject
    {
        private readonly List<Observer> _observers = new List<Observer>();
        private int _int;
        public int Inventory
        {
            get
            {
                return _int;
            }
            set
    {
       // Just to make sure that if there is an increase in inventory then only we are notifying the observers.
          if (value > _int)
             Notify();
          _int = value;
    }
        }
        public void Subscribe(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(Observer observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _observers.ForEach(x => x.Update());
        }
    }

    interface ISubject
    {
        void Subscribe(Observer observer);
        void Unsubscribe(Observer observer);
        void Notify();
    }
}
