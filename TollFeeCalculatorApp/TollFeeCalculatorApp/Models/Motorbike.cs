using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculatorApp.Interfaces;
using TollFeeCalculator.Enums;

namespace TollFeeCalculator.Models
{
    public class Motorbike : Vehicle
    {
        public VehicleType GetVehicleType()
        {
            return VehicleType.Motorbike;
        }
    }
}
