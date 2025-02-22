using TollFeeCalculatorApp.Interfaces;
using TollFeeCalculator.Enums;

namespace TollFeeCalculator.Models
{
    public class Car : Vehicle
    {
        public VehicleType GetVehicleType()
        {
            return VehicleType.Car;
        }
    }
}