using System;
using System.Collections.Generic;
using System.Text;

namespace CV2
{
    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday 
        { 
            get
            {
                if (field == null) field = DateTime.Today;
                return field;
            }
        }

        public Gender Gender { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - Birthday.Year;
                if (Birthday.Date > today.AddYears(-age)) age--;
                return age;
            }
        }


        public Person(string firstName, string lastName) 
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Person(string firstName, string lastName, DateTime birthday, Gender gender = Gender.Unknown)
            : this(firstName, lastName)
        {
            Birthday = birthday;
            Gender = gender;
        }


        public override string ToString()
        {
            return $"{FullName}, {Birthday:dd.MM.yyyy}, age: ({Age}), gender: {Gender}";
        }


    }
}
