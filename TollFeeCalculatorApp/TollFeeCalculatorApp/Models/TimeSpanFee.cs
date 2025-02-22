using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculatorApp.Models
{
    public class TimeSpanFee // created new representation of time intervals with the tollFee
    {
        public TimeSpan start;
        public TimeSpan end;
        public int fee;
    }
}
