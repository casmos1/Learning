using System;

namespace Design_Patterns.Pattern_Factory
{
    class Scooter : IFactory
    {
        public void Drive(int miles)
        {
            Console.WriteLine("Drive the scooter {0} miles.", miles);
        }
    }
}
