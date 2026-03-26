namespace BookRecords.Library
{
    //public record class Book(string Author, string Title, string Publisher, int Year, int Pages, string Language, string Isbn, string Annotation)
    public class Book//(string Author, string Title, string? Publisher, int? Year, string? Isbn, string? Annotation)
    {
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Isbn { get; set; }
        public string? Publisher { get; set; }
        public int? Year { get; set; }
        public string? Annotation { get; set; }

        public Book()
        {
        }

        public Book(string author, string title)
        {
            Author = author;
            Title = title;
        }

        public Book(string author, string title, string? isbn, string? publisher, int? year, string? annotation)
            : this(author, title)
        {
            Isbn = isbn;
            Publisher = publisher;
            Year = year;
            Annotation = annotation;
        }
    }
}