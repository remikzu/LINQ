using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Movie
    {
        public int Amount { get; private set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        int _year;
        public int Year
        {
            get
            {
                Amount++;
                Console.WriteLine($"Returning {_year} for {Title}. In total there are {Amount} instances");
                return _year;
            }
            set
            {
                _year = value;
            }
        }
    }
}
