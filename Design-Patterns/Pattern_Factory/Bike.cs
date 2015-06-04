using System;

namespace Design_Patterns.Pattern_Factory
{
    class Bike : IFactory
    {
        public void Drive(int miles)
        {
            Console.WriteLine("Drive the bike {0} miles.", miles);
        }
    }
}
