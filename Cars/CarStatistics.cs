using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    /// <summary>
    /// Accumulator class for aggregating using extention methods
    /// </summary>
    public class CarStatistics
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }

        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);
            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }
    }
}
