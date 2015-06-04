using System;
using Design_Patterns.Pattern_Factory;
using Design_Patterns.Pattern_Observer;
using Design_Patterns.Pattern_Singleton;

namespace Design_Patterns
{
    class Program
    {
        static void Main(string[] args)
        {
            //ObserverPattern();
            //SingletonPattern();
            FactoryPattern();
        }

        private static void ObserverPattern()
        {
            var subject = new Subject();
            
            // Observer1 takes a subscription to the store
            var observer1 = new Observer("Observer 1");
            subject.Subscribe(observer1);

            // Observer2 also subscribes to the store
            subject.Subscribe(new Observer("Observer 2"));
            subject.Inventory++;

            // Observer1 unsubscribes and Observer3 subscribes to notifications.
            subject.Unsubscribe(observer1);
            subject.Subscribe(new Observer("Observer 3"));
            subject.Inventory++;

            Console.ReadLine();
        }

        private static void SingletonPattern()
        {
            SingletonSample singleton = SingletonSample.InstanceCreation();
            singleton.DisplayMessage();
            Console.ReadLine();
        }

        private static void FactoryPattern()
        {
            VehicleFactory factory = new ConcreteVehicleFactory();

            IFactory scooter = factory.GetVehicle("Scooter");
            scooter.Drive(10);

            IFactory bike = factory.GetVehicle("Bike");
            bike.Drive(20);

            Console.ReadKey();
        }
    }
}
