using System.Runtime.CompilerServices;

namespace CV2
{
    internal class PersonDatabase
    {
        public List<Person> _people = new List<Person>();

        public void Add(params Person[] person)
        {
            if (person != null)
            {
                _people.AddRange(person);
            }
        }

        public void Add(Person person)
        {
            if (person != null)
            {
                _people.Add(person);
            }
        }

        public void Remove(Person person)
        {
            _people.Remove(person);
        }

        public List<Person> Find(string text, Gender? gender = null)
        {
            List<Person> newPeoples = new List<Person>();
            foreach (var person in _people)
            {
                if ((person.FirstName.Contains(text) ||
                    person.LastName.Contains(text) ||
                    person.FullName.Contains(text)) &&
                    (gender == null || person.Gender==gender))
                {
                    newPeoples.Add(person);
                }
            }
            return newPeoples;
        }
    }
}