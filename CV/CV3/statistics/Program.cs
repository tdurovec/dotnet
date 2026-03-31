string path = "numbers.txt";
string[] lines = File.ReadAllLines(path);

Console.WriteLine($"Prvy prvok {lines[0]}");
Console.WriteLine($"Posledny prvok {lines[^1]}");
Console.WriteLine($"Stredny prvok {lines[lines.Length/2]}");

Console.WriteLine();

void PrintStatistics(ReadOnlySpan<int> spanNumbers)
{
    int sum = 0;
    double mean = 0.0;
    double variance = 0.0;

    foreach (var number in spanNumbers)
    {
        sum += number;
    }

    mean = (double)sum / lines.Length;

    double sumSquere = 0.0;
    foreach (var number in spanNumbers)
    {
        sumSquere += Math.Pow(number - mean, 2);
    }
    variance = sumSquere / spanNumbers.Length;

    Console.WriteLine($"Suma = {sum}");
    Console.WriteLine($"Priemer = {mean}");
    Console.WriteLine($"Rozptyl = {variance}");
    Console.WriteLine();
}


var numbersBase = lines.Select(int.Parse).ToArray();
var numbers = (int[])numbersBase.Clone();
Span<int> numbersSpan = numbers.AsSpan();
// Enumerable.ToArray(Enumerable.Select(obsah, int.Parse));

PrintStatistics(numbersSpan);

numbersSpan[..300].Clear();

PrintStatistics(numbersSpan);


numbersSpan[4000..6001].Fill(500);

PrintStatistics(numbersSpan);

// var numbers2 = (int[])numbersBase.Clone();
// var vyrezOd500 = numbers2[5000..];

PrintStatistics(numbersSpan[5000..]);



