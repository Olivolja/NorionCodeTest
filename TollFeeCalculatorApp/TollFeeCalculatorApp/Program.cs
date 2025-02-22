using TollFeeCalculator.Models;
using TollFeeCalculator.Enums;

var tollFeeCalculator = new TollCalculator();

var car = new Car();
DateTime baseDate = new DateTime(2013, 1, 2); // Monday, January 2, 2013

var dates = new DateTime[]
{
    baseDate.AddHours(5).AddMinutes(50), //0 kr
    baseDate.AddHours(6).AddMinutes(20), //8 kr
    baseDate.AddHours(6).AddMinutes(30), //13 kr

    baseDate.AddHours(10), // 8kr
    baseDate.AddHours(10).AddMinutes(10), // 8kr
    baseDate.AddHours(10).AddMinutes(20),// 8kr

    //result should be 13+8 = 21

};

var result = tollFeeCalculator.GetTollFee(car, dates);

Console.WriteLine(result);