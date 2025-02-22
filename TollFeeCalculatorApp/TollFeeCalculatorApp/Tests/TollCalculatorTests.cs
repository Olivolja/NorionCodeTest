using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator.Models;
using Xunit;

namespace TollFeeCalculatorApp.Tests
{
    public class TollCalculatorTests
    {
        [Fact]
        public void TestMultiplePriceChangesInsideSingleInterval()
        {
            var tollFeeCalculator = new TollCalculator();

            var car = new Car();

            DateTime baseDate = new DateTime(2013, 1, 2); // January 2, 2013
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

            Assert.Equal(21, tollFeeCalculator.GetTollFee(car, dates));
        }


        [Fact]
        public void TestMaxTollFee()
        {
            var tollFeeCalculator = new TollCalculator();

            var car = new Car();
            var maxDailyTollFee = 60;

            DateTime baseDate = new DateTime(2013, 1, 2); // January 2, 2013
            var dates = new DateTime[]
            {
                baseDate.AddHours(6), //8 kr
                baseDate.AddHours(7), //18 kr
                baseDate.AddHours(8), //13 kr
                baseDate.AddHours(9), //8 kr
                baseDate.AddHours(10), //8 kr
                baseDate.AddHours(11), //8 kr
                baseDate.AddHours(12), //8 kr
                baseDate.AddHours(13), //8 kr
                baseDate.AddHours(14), //8 kr
                baseDate.AddHours(15), //13 kr
                baseDate.AddHours(16), //18 kr
                baseDate.AddHours(17), //13 kr

            };
            Assert.Equal(maxDailyTollFee, tollFeeCalculator.GetTollFee(car, dates));
        }

        [Fact]
        public void TestTollFreeVehicle()
        {
            var tollFeeCalculator = new TollCalculator();

            var motorbike = new Motorbike();

            DateTime baseDate = new DateTime(2013, 1, 2); //January 2, 2013
            var dates = new DateTime[]
            {
                baseDate.AddHours(5).AddMinutes(50), //0 kr
                baseDate.AddHours(6).AddMinutes(20), //8 kr
                baseDate.AddHours(6).AddMinutes(30), //13 kr
                baseDate.AddHours(10), // 8kr
                baseDate.AddHours(10).AddMinutes(10), // 8kr
                baseDate.AddHours(10).AddMinutes(20),// 8kr
            };

            Assert.Equal(0, tollFeeCalculator.GetTollFee(motorbike, dates));
        }

        [Fact]
        public void TestTollFreeWeekends()
        {
            var tollFeeCalculator = new TollCalculator();

            var car = new Car();

            DateTime baseDate = new DateTime(2025, 2, 22); //february 22, 2025
            var dates = new DateTime[]
            {
                baseDate.AddHours(5).AddMinutes(50), //0 kr
                baseDate.AddHours(6).AddMinutes(20), //8 kr
                baseDate.AddHours(6).AddMinutes(30), //13 kr
                baseDate.AddHours(10), // 8kr
                baseDate.AddHours(10).AddMinutes(10), // 8kr
                baseDate.AddHours(10).AddMinutes(20),// 8kr
            };

            Assert.Equal(0, tollFeeCalculator.GetTollFee(car, dates));
        }

        [Fact]
        public void TestTollFreeHoliday()
        {
            var tollFeeCalculator = new TollCalculator();

            var car = new Car();

            DateTime baseDate = new DateTime(2013, 1, 1); // January 1, 2013, only added holidays for 2013 in this solution
            var dates = new DateTime[]
            {
                baseDate.AddHours(5).AddMinutes(50), //0 kr
                baseDate.AddHours(6).AddMinutes(20), //8 kr
                baseDate.AddHours(6).AddMinutes(30), //13 kr
                baseDate.AddHours(10), // 8kr
                baseDate.AddHours(10).AddMinutes(10), // 8kr
                baseDate.AddHours(10).AddMinutes(20),// 8kr
            };

            Assert.Equal(0, tollFeeCalculator.GetTollFee(car, dates));
        }
    }
}
