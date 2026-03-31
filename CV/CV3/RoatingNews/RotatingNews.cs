
public class RotatingNews 
{
    private string newsText;
    private readonly byte stringRow;
    private readonly ConsoleColor color;

    public RotatingNews(string newsText, byte stringRow, ConsoleColor color)
    {
        this.newsText = newsText + new string(' ', Console.WindowWidth - newsText.Length);
        this.stringRow = stringRow;
        this.color = color;

        Console.ForegroundColor = color;
        Console.CursorVisible = false;
    }

    public async Task run()
    {
        while (true)
        {
            try
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(newsText);
                newsText = newsText.Substring(1) + newsText[0];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }


            // newsText = newsText.Substring(1) + newsText[0];
            await Task.Delay(100);
        }
    }
}