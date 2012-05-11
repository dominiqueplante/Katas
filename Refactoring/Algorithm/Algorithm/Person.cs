using System;

namespace Algorithm
{
    public class Person
    {
        public bool IsYoungerThan(Person otherPerson)
        {
            if (BirthDate < otherPerson.BirthDate)
                return true;
            return false;
        }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}