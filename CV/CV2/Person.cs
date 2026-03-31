using System.Numerics;

namespace CV2
{
    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;
                if (BirthDate.Date > today.AddYears(-age)) age--;
                return age;
            }
        }


        public Person(
            string firstName, 
            string lastName, 
            DateTime birthDate = default, 
            Gender gender = Gender.Unknown
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            if (birthDate == default)
            {
                BirthDate = DateTime.Today;
            }
            else
            {
                BirthDate = birthDate;
            }
        }


        public override string ToString()
        {
            return $"{FullName}, {BirthDate:dd.MM.yyyy}, age: ({Age}), gender: {Gender}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Person other) // kontrola ci je objekt null a rovnakeho typu
            {
                return false;
            }

            return FirstName == other.FirstName &&
                LastName == other.LastName &&
                BirthDate.Date == other.BirthDate.Date &&
                Gender == other.Gender;
        }
    

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, BirthDate, Gender);
    }

    }
}