using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program {
    static List<Book> books = new List<Book>();
    static List<Member> members = new List<Member>();
    static int memberIdCounter = 1;
    const string FILE_PATH = "library_data.json";

    static void Main(string[] args) {
        LoadData();
        while (true) {
            Console.Clear();
            Console.WriteLine("=== Library Management System ===");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add Member");
            Console.WriteLine("3. Borrow Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. View Books");
            Console.WriteLine("6. View Members");
            Console.WriteLine("7. Search Books");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option (1-8): ");

            string choice = Console.ReadLine();

            try {
                switch (choice) {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        AddMember();
                        break;
                    case "3":
                        BorrowBook();
                        break;
                    case "4":
                        ReturnBook();
                        break;
                    case "5":
                        ViewBooks();
                        break;
                    case "6":
                        ViewMembers();
                        break;
                    case "7":
                        SearchBooks();
                        break;
                    case "8":
                        SaveData();
                        Console.WriteLine("Thank you for using the Library Management System!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }

    static void AddBook() {
        Console.Write("Enter ISBN: ");
        string isbn = Console.ReadLine();
        if (string.IsNullOrEmpty(isbn) || books.Exists(b => b.ISBN == isbn)) {
            throw new Exception("Invalid or duplicate ISBN.");
        }

        Console.Write("Enter Title: ");
        string title = Console.ReadLine();
        if (string.IsNullOrEmpty(title) || title.Length < 3) {
            throw new Exception("Title must be at least 3 characters long.");
        }

        Console.Write("Enter Author: ");
        string author = Console.ReadLine();
        if (string.IsNullOrEmpty(author)) {
            throw new Exception("Author cannot be empty.");
        }

        Console.Write("Enter Year Published: ");
        if (!int.TryParse(Console.ReadLine(), out int yearPublished) || yearPublished < 1900 || yearPublished > DateTime.Now.Year) {
            throw new Exception("Invalid year published. It must be a valid year between 1900 and the current year.");
        }

        Book book = new Book(isbn, title, author, yearPublished);
        books.Add(book);
        Console.WriteLine("Book added successfully.");
    }

    static void AddMember() {
        Console.Write("Enter Member Name: ");
        string name = Console.ReadLine();
        if (string.IsNullOrEmpty(name) || name.Length < 3) {
            throw new Exception("Name must be at least 3 characters long.");
        }

        Member member = new Member(memberIdCounter++, name);
        members.Add(member);
        Console.WriteLine($"Member added successfully! Member ID: {member.MemberId}");
    }

    static void BorrowBook() {
        Console.Write("Enter Member ID: ");
        if (!int.TryParse(Console.ReadLine(), out int memberId)) {
            throw new Exception("Invalid Member ID. Should be number.");
        }
        Member member = members.Find(a => a.MemberId == memberId);
        if (member == null) {
            throw new Exception("Member not found!");
        }

        Console.Write("ISBN: ");
        string isbn = Console.ReadLine();
        Book book = books.Find(b => b.ISBN == isbn);
        if (book == null) {
            throw new Exception("Book not found");
        }
        if (!book.IsAvailable) {
            throw new Exception("Book is already checked out.");
        }

        book.IsAvailable = false;
        member.BorrowedBooks.Add(isbn);
        Console.WriteLine("Book checked out successfully.");
    }

    static void ReturnBook() {
        Console.Write("ISBN: ");
        string isbn = Console.ReadLine();
        Book book = books.Find(b => b.ISBN == isbn);
        if (book == null) {
            throw new Exception("Book not found.");
        }
        if (book.IsAvailable) {
            throw new Exception("Book is not borrowed");
        }

        Member member = members.Find(a => a.BorrowedBooks.Contains(isbn));
        if (member != null) {
            member.BorrowedBooks.Remove(isbn);
            book.IsAvailable = true;
            Console.WriteLine("Book returned successfully.");
        }
    }

    static void ViewBooks() {
        if (books.Count == 0) {
            Console.WriteLine("No book yet!");
            return;
        }
        Console.WriteLine("\nBook List:");
        foreach (var book in books) {
            Console.WriteLine(book.ToString());
        }
    }

    static void ViewMembers() {
        if (members.Count == 0) {
            Console.WriteLine("No member yet!");
            return;
        }
        Console.WriteLine("\nMember List:");
        foreach (var member in members) {
            Console.WriteLine(member.ToString());
        }
    }

    static void SearchBooks() {
        Console.Write("Search book: ");
        string keyword = Console.ReadLine().ToLower();
        var result = books.FindAll(b => b.Title.ToLower().Contains(keyword));
        if (result.Count == 0) {
            Console.WriteLine("Book not found");
            return;
        }
        Console.WriteLine("\nResult:");
        foreach (var book in result) {
            Console.WriteLine(book.ToString());
        }
    }

    static void SaveData() {
        var data = new { Book = books, Member = members, IdCounter = memberIdCounter };
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FILE_PATH, json);
    }

    static void LoadData() {
        if (!File.Exists(FILE_PATH)) return;
        string json = File.ReadAllText(FILE_PATH);
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        if (data != null) {
            if (data.TryGetValue("Book", out var bookData)) {
                books = JsonSerializer.Deserialize<List<Book>>(bookData.ToString());
            }
            if (data.TryGetValue("Member", out var memberData)) {
                members = JsonSerializer.Deserialize<List<Member>>(memberData.ToString());
            }
            if (data.TryGetValue("IdCounter", out var idCounter)) {
                memberIdCounter = JsonSerializer.Deserialize<int>(idCounter.ToString());
            }
        }
    }
}