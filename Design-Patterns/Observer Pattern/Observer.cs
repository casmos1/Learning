﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns.Observer_Pattern
{
    public class Observer : IObserver
    {
        public string ObserverName { get; private set; }

        public Observer(string name)
        {
            this.ObserverName = name;
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
