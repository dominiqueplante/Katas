using System;
using System.Collections.Generic;

namespace Algorithm
{
    public class PersonRelationship
    {
        public Person Person1 { get; set; }
        public Person Person2 { get; set; }
        public TimeSpan BirthdayDateDifference { get { return Person2.BirthDate - Person1.BirthDate; } }

        internal static PersonRelationship CreatePair(List<Person> people, int j, int i)
        {
            var personPair = new PersonRelationship();
            setPerson1(personPair, people[i], people[j]);
            setPerson2(personPair, people[j], people[i]);

            return personPair;
        }

        private static void setPerson2(PersonRelationship personPair, Person firstPerson, Person otherPerson)
        {
            if (firstPerson.IsYoungerThan(otherPerson))
            {
                personPair.Person2 = otherPerson;
            }
            else
            {
                personPair.Person2 = firstPerson;
            }
        }

        private static void setPerson1(PersonRelationship personPair, Person firstPerson, Person otherPerson)
        {
            if (firstPerson.IsYoungerThan(otherPerson))
            {
                personPair.Person1 = firstPerson;
            }
            else
            {
                personPair.Person1 = otherPerson;
            }
        }
    }
}