using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("C:\\Users\\remik\\OneDrive\\Pulpit\\Pluralsight\\LINQ Fundamentals\\Cars\\fuel.csv");
            var manufacturers = ProcessManufacturers("C:\\Users\\remik\\OneDrive\\Pulpit\\Pluralsight\\LINQ Fundamentals\\Cars\\manufacturers.csv");

            var query = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .Select(c => c);

            var query2 = from car in cars
                         join manufacturer in manufacturers 
                         on new { car.Manufacturer, car.Year } 
                         equals 
                         new
                         {
                             Manufacturer = manufacturer.Name, manufacturer.Year
                         }
                         orderby car.Combined descending, car.Name ascending
                         select new
                         {
                             manufacturer.Headquarters,
                             car.Name,
                             car.Combined
                         };

            var query3 =
                cars.Join(manufacturers,
                            c => new { c.Manufacturer, c.Year },
                            m => new
                            {
                                Manufacturer = m.Name,
                                m.Year
                            }, (c, m) => new
                            {
                                m.Headquarters,
                                c.Name,
                                c.Combined
                            })
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name);

            /*var top = cars
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .Select(c => c)
                            .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016);*/

            /*var result2 = cars.SelectMany(c => c.Name)
                              .OrderBy(c => c);*/

            foreach (var car in query3.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(line => line.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });
            return query.ToList();
        }

        private static List<Car> ProcessFile(string path)
        {

            /*var query = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select Car.ParseFromCsv(line);*/


            var query = File.ReadAllLines(path)
                       .Skip(1)
                       .Where(line => line.Length > 1)
                       .ToCar();

            return query.ToList();
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3], CultureInfo.InvariantCulture),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
