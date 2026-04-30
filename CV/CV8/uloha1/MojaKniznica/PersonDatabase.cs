namespace MojaKniznica;

using System.Linq;

// TODO 3.08: Vytvorte v programe inštanciu triedy PersonDatabase a pridajte do nej vygenerovanych 50 osob pomocou triedy PersonGenerator
public class PersonDatabase
{
    private readonly List<Person> _people = new List<Person>();

    public void Add(Person person)
    {
        _people.Add(person);
    }

    public void Add(params Person[] people)
    {
        _people.AddRange(people);
    }

    public void Remove(Person person)
    {
        _people.Remove(person);
    }

    // TODO 3.09: Pridajte indexer na čítanie a aj zápis, ktorý vráti inštanciu triedy Person podľa indexu - poradia
    public Person? GetPerson(int idx)
    {
        return _people.ElementAtOrDefault(idx);
    }

    public void SetPerson(Person person, int idx)
    {
        _people.Insert(idx, person);
    }

    // TODO 3.10: Otestujte get časť indexeru v programe, vráťte osobu na indexe 27
    // TODO 3.11: Otestujte set časť indexeru v programe, zapíšte na index 27 novú osobu, ktorú vytvoríte


    // TODO 3.12: Pridajte indexer len na čítanie, ktorý vráti prvú inštanciu triedy Person podľa mena a priezviska.
    // Ak taká nie je, vráti null hodnotu.

    public Person? FindFirst(string text, Gender? gender = null)
    {
        var people = Find(text, gender);
        return people.Count==0 ? null : people[0];
    }

    // TODO 3.13: Otestujte indexer v programe


    public List<Person> Find(string text, Gender? gender = null)
    {
        var result = new List<Person>();

        // Prejdeme vsetky polozky zoznamu a vyhladame v mene
        foreach (var person in _people)
        {
            if (person.FullName.Contains(text))
            {
                if (gender == null || gender.Value == person.Gender)
                    result.Add(person);
            }
        }

        return result;
    }

    // TODO 3.15: Pridajte metódu Sort(), ktorá utriedi osoby
    public void Sort()
    {
        _people.Sort();
    }

    // TODO 3.16: Otestujte metódu Sort() v programe

    public void PrintToConsole()
    {
        foreach (var person in _people)
        {
            Console.WriteLine(person);
        }
    }
}
