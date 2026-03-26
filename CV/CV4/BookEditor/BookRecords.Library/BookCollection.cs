using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace BookRecords.Library
{
    public class BookCollection : ObservableCollection<Book>
    {
        public static BookCollection? LoadFromJson(FileInfo jsonFile)
        {
            using FileStream stream = File.OpenRead(jsonFile.FullName);
            return JsonSerializer.Deserialize<BookCollection>(stream);
        }

        public void SaveToJson(FileInfo jsonFile)
        {
            var content = JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(jsonFile.FullName, content);
        }

        public void SaveToCsv(FileInfo csvFile, string[]? columns = null, string delimiter = "\t")
        {
            // Pre jednoduchosť netreba riešiť veľkosť písmen stĺpcov

            //var properties = new[] { "Author", "Title", "Publisher", "Year", "Isbn", "Annotation" };
            var properties = new[] { nameof(Book.Author), nameof(Book.Title), nameof(Book.Isbn), nameof(Book.Publisher), nameof(Book.Year), nameof(Book.Annotation) };
            if (columns == null || columns.Length == 0)
                columns = properties;

            var result = new StringBuilder();

            // Zapíšeme hlavičku
            result.AppendLine(string.Join(delimiter, columns));

            // Zapíšeme záznamy
            foreach (var book in this)
            {
                foreach (var column in columns)
                {
                    switch (column)
                    { 
                        case nameof(Book.Author):
                            result.Append($"{book.Author}{delimiter}");
                            break;
                        case nameof(Book.Title):
                            result.Append($"{book.Title}{delimiter}");
                            break;
                        case nameof(Book.Publisher):
                            result.Append($"{book.Publisher}{delimiter}");
                            break;
                        case nameof(Book.Year):
                            result.Append($"{book.Year}{delimiter}");
                            break;
                        case nameof(Book.Isbn):
                            result.Append($"{book.Isbn}{delimiter}");
                            break;
                        case nameof(Book.Annotation):
                            result.Append($"{book.Annotation}{delimiter}");
                            break;
                    }
                }

                result.AppendLine();
            }

            File.WriteAllText(csvFile.FullName, result.ToString());
        }
    }
}
