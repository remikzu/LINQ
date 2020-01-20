using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateXml();
            QueryXml();
        }

        private static void QueryXml()
        {
            var document = XDocument.Load("C:\\Users\\remik\\OneDrive\\Pulpit\\Pluralsight\\LINQ Fundamentals\\Cars\\fuelattribute.xml");

            var querySyntax =
                from element in document.Element("Cars").Elements("Car")
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;

            var methodSyntax =
                document.Element("Cars").Elements("Car")
                .Where(d => d.Attribute("Manufacturer").Value == "BMW")
                .Select(d => d.Attribute("Name").Value);

            foreach (var name in querySyntax)
            {
                Console.WriteLine(name);
            }
        }



        private static void CreateXml()
        {
            var records = ProcessCars("C:\\Users\\remik\\OneDrive\\Pulpit\\Pluralsight\\LINQ Fundamentals\\Cars\\fuel.csv");
            //var manufacturers = ProcessManufacturers("C:\\Users\\remik\\OneDrive\\Pulpit\\Pluralsight\\LINQ Fundamentals\\Cars\\manufacturers.csv");

            var document = new XDocument();
            var cars = new XElement("Cars");

            var elements =
                from record in records
                select new XElement("Car",
                            new XAttribute("Name", record.Name),
                            new XAttribute("Combined", record.Combined),
                            new XAttribute("Manufacturer", record.Manufacturer));

            cars.Add(elements);
            document.Add(cars);
            document.Save("C:\\Users\\remik\\OneDrive\\Pulpit\\Pluralsight\\LINQ Fundamentals\\Cars\\fuelattribute.xml");
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

        private static List<Car> ProcessCars(string path)
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
