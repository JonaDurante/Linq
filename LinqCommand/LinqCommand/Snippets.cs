using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Xml.Linq;

namespace LinqCommand
{
    public class Snippets
    {
        static public void BasicLinq() 
        {
            string[] cars = {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat Leon"
            };

            //1. Select *
            //A
            var Carlist = from car in cars select car;
            foreach ( var car in Carlist ) { 
                Console.WriteLine( car ); 
            }
            //B
            var Carlist2 = cars.Select(x => x.ToString()).ToList();
            foreach (var car in Carlist2)
            {
                Console.WriteLine(car);
            }
            //2 Select Where
            //A
            var audList = from car in cars where car.Contains("Audi") select car;
            foreach (var car in audList)
            {
                Console.WriteLine(car);
            }
            //B
            var audList2 = cars.Where(x => x.Contains("Audi"));
            foreach (var car in audList)
            {
                Console.WriteLine(car);
            }
        }
        static public void LinqNumber() 
        { 
            List<int> number = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // a - for each number multiply by 3
            // b - take all number but 9
            // c - Ordernumberby ascending values
            var processedNumberList = 
                number
                .Select(num => num * 3)         // -a 
                .Where(num => num != 9)         // -b 
                .OrderBy(num => num).ToList();  // -c
        }

        static void SeacrhExamples()
        {
            List<string> textList = new List<string>
            {
                "a","bx","c","d","e","cj","f","c"
            };

            // 1. Fist of the elements
            var firts = textList.First();

            // 2. First element that is "c"
            var elementC = textList.First(text => text.Equals("c"));

            // 3. First element that contains "j"
            var Fistj = textList.First(text => text.Contains("j"));

            // 4. First element that contains z or default return "a"
            var fistOrDefault = textList.FirstOrDefault(text => text.Contains("z"), "a");

            // 5. Last element that contains z or default empty list
            var LastOrDifault = textList.LastOrDefault(text => text.Contains("z"));

            // 6. Single values
            var SingleValues = textList.Single();
            var SingleOrDefaultValues = textList.SingleOrDefault();

            int[] evenNumer = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumer = { 0, 2, 6 };

            // Obtein (4,6)
            var myEvenNumber = evenNumer.Except(otherEvenNumer); // return 4 y 6 que no estan en oternumber
        }

        static public void MultipleSelects()
        {
            string[] myOpinions =
            {
                "opinion 1, text 1",
                "opinion 2, text 2",
                "opinion 3, text 3",               
            };

            //Select Many
            var opinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));
            // ------------------------
            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[] 
                    {
                        new Employee()
                        {
                            Id = 1,
                            Name = "Martin",
                            Enail = "martin@algungroup.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id = 2,
                            Name = "pepe",
                            Enail = "pepe@algungroup.com",
                            Salary = 1000
                        },
                        new Employee()
                        {
                            Id = 3,
                            Name = "Juanjo",
                            Enail = "Juanjo@algungroup.com",
                            Salary = 2000
                        },
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 4,
                            Name = "Ana",
                            Enail = "Ana@algungroup.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id = 5,
                            Name = "Maria",
                            Enail = "Maria@algungroup.com",
                            Salary = 1500
                        },
                        new Employee()
                        {
                            Id = 6,
                            Name = "Marta",
                            Enail = "Marta@algungroup.com",
                            Salary = 4000
                        },
                    }
                }

            };

            //Obtein all employees of all enterprises
            var employeesLis = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Knows if an a List is empty
            var hasEnterprises = enterprises.Any();
            // has employees
            var hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());
            //All Enteroruses at keast has an enployee with at least than 1000
            var moreThanMil = enterprises.Any(enterprise => enterprise.Employees.Any(employee => employee.Salary >= 1000));
        }

        static public void LinqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // Inner Join
            var innerList = from element in firstList
                            join secondElement in secondList
                            on element equals secondElement
                            select new { element, secondElement };
            
            var innerList2 =firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement }
                    );

            // Outer join - Left
            var leftOuterJoin = from element in firstList
                                   join secondElement in secondList
                                   on element equals secondElement
                                   into temporalList
                                   from temporalElement in temporalList.DefaultIfEmpty()
                                   where element != temporalElement
                                   select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondelement in secondList.Where(x => x == element).DefaultIfEmpty()
                                 select new { Element = element, Secondelement = secondelement };


            // Outer join - Righ
            var RigthOuterJoin = from secondElement in secondList
                                    join element in firstList
                                    on secondElement equals element
                                    into temporalList
                                    from temporalElement in temporalList.DefaultIfEmpty()
                                    where secondElement != temporalElement
                                    select new { Element = secondElement };

            // Union
            var unionList = leftOuterJoin.Union(RigthOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            { 
                1,2,3,4,5,6,7,8,9,10
            };
            // SKIP
            // selecciono todo excepto los dos primeros elementos
            var skipTwoFirstValues = myList.Skip(2);

            // selecciono todo excepto los dos últimos elementos
            var skipLastTwo = myList.SkipLast(2);

            // selecciono todo los elementos mayores a 4
            var skipWhile = myList.SkipWhile(x => x < 4);

            // TAKE
            //selecciono los primeros dos elementos
            var takeFirstTwo = myList.Take(2);

            //seleccioto los ultimos dos elementos
            var takeLastTwo = myList.TakeLast(2);

            // selecciono los mayores a 4
            var smallerhanfor = myList.TakeWhile(x => x < 4);
        }

        // -------------------------- ToDo --------------------------
        // Variables

        // Zip

        // Repeat

        // All

        // Aggregate

        // Disctinct

        // GroupBy
    }
}