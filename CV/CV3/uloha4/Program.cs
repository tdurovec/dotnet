
using System.Diagnostics;

string[] lines = File.ReadAllLines("Operations-input.txt");
int N = int.Parse(lines[0]);

double[] numbers = new double[N];
for (int i = 1; i < lines.Length; i++)
{
    string line = lines[i];
    string[] vals = line.Split();
    int a = int.Parse(vals[0]);
    int b = int.Parse(vals[1]);
    MathOperation o = convert_from_symbol(vals[2]);
    int v = int.Parse(vals[3]);
    makeOperation(a, b, o, v);
}


void makeOperation(int a, int b, MathOperation o, int v)
{
    for (int i = a; i <= b ; i++)
    {
        numbers[i] = o.Apply(numbers[i], v);
    }
}

MathOperation convert_from_symbol(string symbol)
{
    return symbol switch
    {
        "+" => MathOperation.Add,
        "-" => MathOperation.Subtract,
        "*" => MathOperation.Multiply,
        "/" => MathOperation.Divide,
        _   => throw new ArgumentOutOfRangeException(nameof(symbol))
    };
}

double[] output = File.ReadAllLines("Operations-output.txt").Select(double.Parse).ToArray();

bool isEqual = true;

if (output.Length != numbers.Length)
{
    isEqual = false;
} else
{

    for (int i = 0; i < N; i++)
    {
        if (output[i] != numbers[i])
        {
            isEqual = false;
            break;
        }
    }
}

Console.WriteLine(isEqual);