using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;

namespace KR_part3
{
    internal class Program
    {
        public class Person : IComparable
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public override string ToString()
            {
                return Name + " : " + Age;
            }

            public int CompareTo(object a)
            {
                Person p = a as Person;
                if (this.Age.Equals(p.Age))
                    return this.Name.CompareTo(p.Name);
                else
                    return this.Age.CompareTo(p.Age);
            }
        }

        public class People :  ISortable, IEnumerable
        {
            private Person[] persons;

            public void Sort()
            {
                Array.Sort(persons);
            }

            public void ReadFromFile(string name)
            {
                string[] lines = System.IO.File.ReadAllLines(name);
                persons = new Person[lines.Length];

                int i = 0;
                foreach (string line in lines)
                {
                    var nameAge = line.Split(' ');
                    string pName = nameAge[0];
                    int pAge = int.Parse(nameAge[1]);
                    Person pers = new Person(pName, pAge);
                    persons[i++] = pers;
                }
            }

            public IEnumerator GetEnumerator()
            {
                return persons.GetEnumerator();
            }
        }

        public interface ISortable
        {
            void Sort();
        }
        
        public static void Main(string[] args)
        {
            People person_list = new People();
            person_list.ReadFromFile(@"C:\Users\Danil\RiderProjects\KR_part3\KR_part3\persons.txt");

            foreach (Person pers in person_list)
            {
                Console.WriteLine(pers.ToString());
            }

            person_list.Sort();

            Console.WriteLine("\nSorted:");
            foreach (Person pers in person_list)
            {
                Console.WriteLine(pers.ToString());
            }
        }
    }
}