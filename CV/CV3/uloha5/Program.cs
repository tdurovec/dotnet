using System.Numerics;

string sentense = "Although C# is derived from the C programming language, it introduces some unique and powerful features, such as delegates (which can be viewed as type-safe function pointers) and lambda expressions";

Dictionary<char, int> lettersCount = new Dictionary<char, int>();


foreach (char letter in sentense)
{
    if (letter == ' ') continue;
    if (lettersCount.ContainsKey(letter))
    {
        lettersCount[letter]++; 
    } 
    else
    {
        lettersCount.Add(letter, 1);
    }
}


foreach (var letterCount in lettersCount.OrderByDescending(x => x.Value))
{
   Console.WriteLine($"Letter: {letterCount.Key} Count: {letterCount.Value}"); 
}


void BigInt()
{
    BigInteger a = BigInteger.Parse("1234567890123456789");
    Console.WriteLine($"Súčin:   {sucin}");
    Console.WriteLine($"Podiel:  {podiel}");
    Console.WriteLine($"Zvyšok:  {zvysok}");

    Console.WriteLine("\n--- Porovnanie ---");

    Console.WriteLine($"a == b : {a == b}");
    Console.WriteLine($"a != b : {a != b}");
    Console.WriteLine($"a < b  : {a < b}");
    Console.WriteLine($"a > b  : {a > b}");
    Console.WriteLine($"a <= b : {a <= b}");
    Console.WriteLine($"a >= b : {a >= b}");
}