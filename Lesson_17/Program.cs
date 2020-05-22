using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Schema;
using System.ComponentModel;

namespace Lesson_17
{
    class Program
    {
        static void Main(string[] args)
        {
            #region      Linq query version

            //var persons = new List<Person>
            //{
            //    new Person{ Name= "Anders", SurName = "Hejlsberg", Age = 60},
            //    new Person{ Name= "James", SurName = "Gosling", Age = 65},
            //    new Person{ Name= "Bjarne", SurName = "Stroustrup", Age = 70},
            //    new Person{ Name= "Richard", SurName = "Stallman", Age = 60},
            //};


            ////var personquery = from person in persons
            ////                  where person.Name.Length > 6 && person.Age < 70
            ////                  select person;


            ////var personquery = from person in persons
            ////                  orderby person.Name.Length 
            ////                  select person;

            //var groupPerson = from person in persons
            //                  orderby person.Age descending
            //                  group person by person.Age;

            //foreach (var item in groupPerson)
            //{
            //    Console.WriteLine("Group persons age is = "+ item.Key);

            //    foreach (var person in item)
            //    {
            //        Console.WriteLine(person);
            //    }
            //}


            //foreach (var item in personquery)
            //{
            //    Console.WriteLine(item);
            //}

            var progLanguages = new List<ProgrammingLanguage>
            {
                new ProgrammingLanguage{ Id = 1, Name = "c#"},
                new ProgrammingLanguage{ Id = 2, Name = "c++"},
                new ProgrammingLanguage{ Id = 3, Name = "java"},
                new ProgrammingLanguage{ Id = 4, Name = "python"},
            };

            var engineers = new List<SoftwareEngineer>
            {
                new SoftwareEngineer { FullName = "Anders Helserberg", LanguageCreatedById = 1},
                new SoftwareEngineer { FullName = "James Gosling", LanguageCreatedById = 3},
                new SoftwareEngineer { FullName = "Bjarne Strostrup", LanguageCreatedById = 2},
                new SoftwareEngineer { FullName = "Guide van Rossum", LanguageCreatedById = 4},
                new SoftwareEngineer { FullName = "AngularJS", LanguageCreatedById = 5},
            };


            //var joinQuery = from engineer in engineers
            //                join program in progLanguages on engineer.LanguageCreatedById equals program.Id
            //                select new { EngineerFullName = engineer.FullName, ProgramLanguage = program.Name};


            //foreach (var item in joinQuery)
            //{
            //    Console.WriteLine(item.EngineerFullName + " " + item.ProgramLanguage);
            //}

            #endregion

            #region     Linq method version

            var persons = new List<Person>
            {
                new Person{ Name= "Anders", SurName = "Hejlsberg", Age = 60},
                new Person{ Name= "James", SurName = "Gosling", Age = 65},
                new Person{ Name= "Bjarne", SurName = "Stroustrup", Age = 70},
                new Person{ Name= "Richard", SurName = "Stallman", Age = 60},
            };

            var max = persons.Max(item => item.Age);

            Console.WriteLine("Max age is person " + max);

            var min = persons.Min(item => item.Age);

            Console.WriteLine("Min age is person " + min);

            var sumAge = persons.Sum(item => item.Age);

            Console.WriteLine("Sum al ages = "  +sumAge);

            var averageAge = persons.Average(item => item.Age);
            Console.WriteLine("All age's average  = " + averageAge);

            
            Console.WriteLine("All Person count" + persons.Count());


            Console.WriteLine("60 greate then > count = " + persons.Where(item => item.Age > 60).Count());  // O(N)


            Console.WriteLine("All person elements");
            foreach (var item in persons.OrderByDescending(item => item.Age))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Grouping all persons");

            foreach (var item in persons.OrderBy(item => item.Age).GroupBy(key => key.Age))
            {
                Console.WriteLine($"Person {item.Key} is persons");

                foreach (var person in item)
                {
                    Console.WriteLine(person);
                }
            }

            persons.Any(item => item.Age > 60);

            var newselectpersons = persons.Where(item => item.Age > 65).Select(item => new
            {
                PersonFullName = item.Name + " " + item.SurName,
                PersonAge = item.Age
            });

            Console.WriteLine("Select person with method versions");
            foreach (var item in newselectpersons)
            {
                Console.WriteLine(item.PersonFullName + " " + item.PersonAge);
            }

            Console.WriteLine("Join operation with method(softeng, progLang)");

            var joinProgLangSoftEngin = engineers.Join(progLanguages,
                                                       en => en.LanguageCreatedById,
                                                       la => la.Id,
                                                       (en, la) => new { EngineerName = en.FullName, ProgrammingLanguage = la.Name });

            foreach (var item in joinProgLangSoftEngin)
            {
                Console.WriteLine(item.EngineerName + " " + item.ProgrammingLanguage);
            }


            Console.WriteLine("First or defualt");
            var perdefault = persons.FirstOrDefault();
            Console.WriteLine(perdefault.ToString());

            Console.WriteLine("Greate then 65 age > single method");
            var singleelement= persons.SingleOrDefault(item => item.Age > 65);
            Console.WriteLine(singleelement);


            var pagenationperson =  persons.Skip(2).Take(2);

            foreach (var item in pagenationperson)
            {
                Console.WriteLine(item.ToString());
            }


            var perArr = persons.ToArray(); // O(N)


            //persons.ForEach(item =>
            //{
            //    Console.WriteLine(item.Name + " " + item.SurName);
            //});


            #endregion

            Console.ReadKey();
        }
    }



    public class ProgrammingLanguage
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class SoftwareEngineer
    {
        public string FullName { get; set; }

        public int LanguageCreatedById { get; set; }

        public override string ToString()
        {
            return  $"{FullName}";
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public short Age { get; set; }

        public override string ToString()
        {
            return this.Name + " " + this.SurName + " " + this.Age;
        }


        public override bool Equals(object obj)
        {
            var person = obj as Person;

            if (person != null)
            {
                return person.Age == this.Age;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
