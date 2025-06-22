public class Book {
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearPublished { get; set; }
    public bool IsAvailable { get; set; }

    public Book (string isbn, string title, string author, int yearPublished) {
        ISBN = isbn;
        Title = title;
        Author = author;
        YearPublished = yearPublished;
        IsAvailable = true; // Default
    }

    public override string ToString() {
        return $"ISBN: {ISBN}, Title: {Title}, Author: {Author}, Year Published: {YearPublished}, Status: {(IsAvailable ? "Available" : "Checked Out")}";
    }
}