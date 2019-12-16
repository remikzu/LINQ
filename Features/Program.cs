using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Remigiusz"},
                new Employee { Id = 2, Name = "Artur"}
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 2, Name = "Radoslaw"}
            };

            Console.WriteLine(developers.Count());

            //Using lambdas
            foreach (var employee in developers.Where(e => e.Name.StartsWith("R")))
            {
                Console.WriteLine(employee.Name);
            }

            //Using delegates
            foreach (var employee in developers.Where(
                delegate (Employee employee)
                {
                    return employee.Name.StartsWith("R");
                }))
            {
                Console.WriteLine(employee.Name);
            }
            //Using own method
            foreach (var employee in developers.Where(NameStartsWithS))
            {
                Console.WriteLine(employee.Name);
            }

            IEnumerator<Employee> enumerator = developers.GetEnumerator();
            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }

        }

        private static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("R");
        }
    }
}
