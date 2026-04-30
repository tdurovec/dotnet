namespace MojaKniznica;

public class PersonGenerator
{
    private static readonly string[] MaleFirstNames = new[] { "Ján", "Alexander", "Adam", "Juraj", "Štefan" };
    private static readonly string[] MaleLastNames = new[] { "Nový", "Malý", "Veľký", "Chudý", "Vysoký", "Bohatý", "Krásny" };
    private static readonly string[] FemaleFirstNames = { "Silvia", "Františka", "Michaela", "Barbora", "Eva" };
    private static readonly string[] FemaleLastNames = new[] { "Nová", "Malá", "Veľká", "Chudá", "Vysoká", "Bohatá", "Krásna" };

    private static Random random = new Random(123);

	public static Person[] Generate(int count)
	{
		var result = new Person[count];
		for (int i = 0; i < count; i++)
		{
            Gender gender;
            string firstName, lastName;
						
			if (random.Next(2) == 0)
			{
				gender = Gender.Male;
				firstName = MaleFirstNames[random.Next(MaleFirstNames.Length)];
				lastName = MaleLastNames[random.Next(MaleLastNames.Length)];
			}
			else
			{
				gender = Gender.Female;
				firstName = FemaleFirstNames[random.Next(FemaleFirstNames.Length)];
				lastName = FemaleLastNames[random.Next(FemaleLastNames.Length)];
			}

            int age = random.Next(0, 121); // Vek od nula po 120 rokov
            var birthdate = DateTime.Now.AddYears(-age);

            result[i] = new Person(firstName, lastName, birthdate, gender);
		}

		return result;
	}
}
