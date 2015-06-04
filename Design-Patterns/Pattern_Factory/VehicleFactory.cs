namespace Design_Patterns.Pattern_Factory
{
    public abstract class VehicleFactory
    {
        public abstract IFactory GetVehicle(string vehicle);

    }
}
