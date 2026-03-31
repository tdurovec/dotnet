public static class MathOperationExtensions
{
    public static double Apply(this MathOperation op, double x, double y)
    {
        return op switch
        {
            MathOperation.Add      => x + y,
            MathOperation.Subtract => x - y,
            MathOperation.Multiply => x * y,
            MathOperation.Divide   => y != 0 ? x / y : throw new DivideByZeroException(),
            _                      => throw new ArgumentOutOfRangeException(nameof(op))
        };
    }

}