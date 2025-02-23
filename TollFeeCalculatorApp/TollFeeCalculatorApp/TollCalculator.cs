using System;
using System.Globalization;
using TollFeeCalculatorApp.Interfaces;
using TollFeeCalculator.Enums;
using TollFeeCalculator.Models;
using TollFeeCalculatorApp.Models;


public class TollCalculator
{
    public const int MaxDailyTollFee = 60; // Added as const for class uniformity and extendability

    // Grouped public methods above private methods, and paired with names

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    
    public int GetTollFee(IVehicle vehicle, DateTime[] dates) 
    {
        if (vehicle is null // Added nullchecks for public method
            || dates is null
            || dates.Length <= 0)
        {
            return 0;
        } 

        DateTime intervalStart = dates[0];
        int totalFee = 0;
        int currentMaxFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(vehicle, date);
            int intervalStartFee = GetTollFee(vehicle, intervalStart);

            currentMaxFee = Math.Max(currentMaxFee, intervalStartFee);

            double minutes = (date - intervalStart).TotalMinutes; // Simplified the calculation

            if (minutes <= 60)
            {
                
                totalFee += Math.Max(nextFee, intervalStartFee) - (totalFee > 0 ? currentMaxFee : 0); // Performs the calculation in a single transaction instead of subrtracting and adding the tempfee in the end
                currentMaxFee = Math.Max(nextFee, intervalStartFee); // update currentMaxFee since otherwise multiple price changes within the same interval will add the difference compared to the start price

            }
            else
            {
                totalFee += nextFee;
                intervalStart = date; // Added new intervalStart because a new interval has started
                currentMaxFee = 0; // reset currentMaxFee since new interval is starting
            }
            
        }

        
        return Math.Min(totalFee, MaxDailyTollFee); // Returns the minimum value of the total and 60 since 60 is the max price for a day

    }

    private int GetTollFee(IVehicle vehicle, DateTime date) // swaped parameter order inorder to be consistent with similar method for multiple dates, also made method private
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        // Loop over the list inorder to find the interval and its fee, more maintanable code since method does not need to change when adding/changeing timeSpanFees
        foreach (var interval in timeSpanFees)
        {
            if (date.TimeOfDay >= interval.start && date.TimeOfDay <= interval.end)
            {
                return interval.fee;
            }
        }
        return 0;
    }

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        VehicleType vehicleType = vehicle.GetVehicleType();

        return tollFreeVehicles.Contains(vehicleType); // checks the static readonly hashset if the vehicleType is a tollFree Vehicle
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek == DayOfWeek.Saturday 
            || date.DayOfWeek == DayOfWeek.Sunday 
            || date.Month == 7) return true; // added juli as a free moth with Month == 7

        return holidays.Contains(date.Date); // checks the static readonly hashset if the Date is a Holiday
    }

    private static readonly HashSet<VehicleType> tollFreeVehicles = new HashSet<VehicleType> // created a HashSet for performance lookup and maintainability
    {
        VehicleType.Motorbike,
        VehicleType.Tractor,
        VehicleType.Emergency,
        VehicleType.Diplomat,
        VehicleType.Foreign,
        VehicleType.Military
    };

    private static readonly HashSet<DateTime> holidays = new HashSet<DateTime> // created a HashSet for performance lookup and maintainability
    {
        new DateTime(2013, 1, 1).Date,
        new DateTime(2013, 3, 28).Date,
        new DateTime(2013, 3, 29).Date,
        new DateTime(2013, 4, 1).Date,
        new DateTime(2013, 4, 30).Date,
        new DateTime(2013, 5, 1).Date,
        new DateTime(2013, 5, 8).Date,
        new DateTime(2013, 5, 9).Date,
        new DateTime(2013, 6, 5).Date,
        new DateTime(2013, 6, 6).Date,
        new DateTime(2013, 6, 21).Date,
        new DateTime(2013, 7, 1).Date,
        new DateTime(2013, 11, 1).Date,
        new DateTime(2013, 12, 24).Date,
        new DateTime(2013, 12, 25).Date,
        new DateTime(2013, 12, 26).Date,
        new DateTime(2013, 12, 31).Date
    };

    private static readonly List<TimeSpanFee> timeSpanFees = new List<TimeSpanFee> // added a List inorder to keep timespan intervals and fees sorted to avoid multiple if statements for maintanability
    {
        new TimeSpanFee { start = new TimeSpan(6, 0, 0), end = new TimeSpan(6, 29, 59), fee = 8 },
        new TimeSpanFee { start = new TimeSpan(6, 30, 0), end = new TimeSpan(6, 59, 59), fee = 13 },
        new TimeSpanFee { start = new TimeSpan(7, 0, 0), end = new TimeSpan(7, 59, 59), fee = 18 },
        new TimeSpanFee { start = new TimeSpan(8, 0, 0), end = new TimeSpan(8, 29, 59), fee = 13 },
        new TimeSpanFee { start = new TimeSpan(8, 30, 0), end = new TimeSpan(14, 59, 59), fee = 8 },
        new TimeSpanFee { start = new TimeSpan(15, 0, 0), end = new TimeSpan(15, 29, 59), fee = 13 },
        new TimeSpanFee { start = new TimeSpan(15, 30, 0), end = new TimeSpan(16, 59, 59), fee = 18 },
        new TimeSpanFee { start = new TimeSpan(17, 0, 0), end = new TimeSpan(17, 59, 59), fee = 13 },
        new TimeSpanFee { start = new TimeSpan(18, 0, 0), end = new TimeSpan(18, 29, 59), fee = 8 }
    };
}