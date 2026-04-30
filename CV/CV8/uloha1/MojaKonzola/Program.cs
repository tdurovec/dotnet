using MojaKniznica;

var p = new Person("jan", "mrkvicka", null);

var personalDatabase = new PersonDatabase();

Person[] people = PersonGenerator.Generate(50);

personalDatabase.Add(people);

// foreach (var personn in people)
// {
//     System.Console.WriteLine(personn.ToString());    
// }

var person = personalDatabase.FindFirst("Ad");
if (person != null)
{

    System.Console.WriteLine(person.ToString());
}
