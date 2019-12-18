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

            /*Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) =>
            {
                int temp = x + y;
                return temp;
            };

            Action<int> write = x => Console.WriteLine(x);

            write(square(add(3, 5)));

            Console.WriteLine(square(add(3, 5)));*/

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Remigiusz"},
                new Employee { Id = 2, Name = "Artur"}
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 2, Name = "Radoslaw"}
            };

            //Console.WriteLine(developers.Count());

            var query = developers.Where(e => e.Name.Length == 5)
                                  .OrderBy(e => e.Name);

            var query2 = from developer in developers
                        where developer.Name.Length == 5
                        orderby developer.Name
                        select developer;

            //Using lambdas
            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);
            }
            /*
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
            }*/

        }

        private static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("R");
        }
    }
}
