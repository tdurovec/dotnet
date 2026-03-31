var RotatingNews = new RotatingNews("text", 50, ConsoleColor.Blue);
// await RotatingNews.run();

void RotateStringArray(string[] strArr, int step = 1, bool left = true)
{
    if (left)
    {
        for (int i = 0; i < step; i++)
        {
            var store = strArr[0];
            for (int j = 1; j < strArr.Length; j++)
            {
                strArr[j-1] = strArr[j];
            }
            strArr[^1] = store;
        }
    } else
    {
        for (int i = 0; i < step; i++)
        {
            var store = strArr[^1];
            for (int j = strArr.Length-2; j >= 0; j--)
            {
                strArr[j+1] = strArr[j];
            }
            strArr[0] = store;
        }
    }

    foreach (var value in strArr)
    {
        System.Console.WriteLine(value);
    }
}


var text = new string[] {"A", "B", "C", "D", "E"};

// RotateStringArray(text, text.Length + 1, false);




