using System;
using System.Collections.Generic;

// Interface
interface IBook
{
    double GetPrice();
    void Display();
}

// Base class
abstract class Book : IBook
{
    // Static field
    public static int TotalBooks = 0;

    protected string Title;
    protected string Author;
    protected double Price;

    // Constructor
    public Book(string title, string author, double price)
    {
        Title = title;
        Author = author;
        Price = price;
        TotalBooks++;
    }

    // Copy Constructor
    public Book(Book other)
    {
        Title = other.Title;
        Author = other.Author;
        Price = other.Price;
    }

    // Static method
    public static void ShowTotal()
    {
        Console.WriteLine($"Total books in system: {TotalBooks}");
    }

    public virtual double GetPrice() => Price;

    public abstract void Display();

    // Operator Overloading - comparison
    public static bool operator ==(Book a, Book b)
    {
        return a.Price == b.Price;
    }

    public static bool operator !=(Book a, Book b)
    {
        return !(a == b);
    }

    // Operator Overloading - applying discount
    public static Book operator *(Book book, double discount)
    {
        book.Price = book.Price * (1 - discount);
        return book;
    }

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();
}

// Ashifur-Rahman
// Fiction book - Inherits from Book
class FictionBook : Book
{
    private string Genre;

    // Constructor
    public FictionBook(string title, string author, double price, string genre) 
        : base(title, author, price)
    {
        Genre = genre;
    }

    // Constructor Overloading - without genre
    public FictionBook(string title, string author, double price) 
        : base(title, author, price)
    {
        Genre = "Unknown";
    }

    // Copy Constructor
    public FictionBook(FictionBook other) : base(other)
    {
        Genre = other.Genre;
    }

    public override void Display()
    {
        Console.WriteLine($"[FICTION] {Title} by {Author} | Genre: {Genre} | Price: ${Price:F2}");
    }
}

// Mahabuba-Mim
// Non-Fiction book - Inherits from Book
class NonFictionBook : Book
{
    private string Category;

    // Constructor
    public NonFictionBook(string title, string author, double price, string category) 
        : base(title, author, price)
    {
        Category = category;
    }

    // Constructor Overloading - without category
    public NonFictionBook(string title, string author, double price) 
        : base(title, author, price)
    {
        Category = "General";
    }

    // Copy Constructor
    public NonFictionBook(NonFictionBook other) : base(other)
    {
        Category = other.Category;
    }

    public override void Display()
    {
        Console.WriteLine($"[NON-FICTION] {Title} by {Author} | Category: {Category} | Price: ${Price:F2}");
    }
}

// Homayed-Saidi
class Bookstore
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine("✓ Book added!");
    }

    public void DisplayAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books in inventory.");
            return;
        }

        Console.WriteLine("\n--- Books in Inventory ---");
        for (int i = 0; i < books.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            books[i].Display();
        }
    }

    public void CompareBooks()
    {
        if (books.Count < 2)
        {
            Console.WriteLine("Need at least 2 books to compare!");
            return;
        }

        Console.Write("Enter first book number: ");
        if (int.TryParse(Console.ReadLine(), out int idx1) && idx1 > 0 && idx1 <= books.Count)
        {
            Console.Write("Enter second book number: ");
            if (int.TryParse(Console.ReadLine(), out int idx2) && idx2 > 0 && idx2 <= books.Count)
            {
                // Operator Overloading
                if (books[idx1 - 1] == books[idx2 - 1])
                    Console.WriteLine("Both books have the same price!");
                else
                    Console.WriteLine("Books have different prices.");
            }
            else
                Console.WriteLine("Invalid choice!");
        }
        else
            Console.WriteLine("Invalid choice!");
    }

    public void ApplyDiscount()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available!");
            return;
        }

        Console.Write("Enter book number: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= books.Count)
        {
            Console.Write("Enter discount (0.1 for 10%): ");
            if (double.TryParse(Console.ReadLine(), out double discount))
            {
                // Operator Overloading - discount
                books[idx - 1] = books[idx - 1] * discount;
                Console.WriteLine("✓ Discount applied!");
                books[idx - 1].Display();
            }
            else
                Console.WriteLine("Invalid input!");
        }
        else
            Console.WriteLine("Invalid choice!");
    }

    public void CopyBook()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available!");
            return;
        }

        Console.Write("Enter book number to copy: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= books.Count)
        {
            // Copy Constructor
            if (books[idx - 1] is FictionBook)
            {
                var copy = new FictionBook((FictionBook)books[idx - 1]);
                AddBook(copy);
            }
            else if (books[idx - 1] is NonFictionBook)
            {
                var copy = new NonFictionBook((NonFictionBook)books[idx - 1]);
                AddBook(copy);
            }
        }
        else
            Console.WriteLine("Invalid choice!");
    }
}

// Forhath-Hosain-Ome
class Program
{
    static void Main()
    {
        Console.WriteLine("=== Bookstore Management System ===\n");
        Bookstore store = new Bookstore();

        // Add sample books
        store.AddBook(new FictionBook("The Hobbit", "J.R.R. Tolkien", 15.99, "Fantasy"));
        store.AddBook(new NonFictionBook("Sapiens", "Yuval Noah Harari", 18.50, "History"));

        while (true)
        {
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1. View All Books");
            Console.WriteLine("2. Add Fiction Book");
            Console.WriteLine("3. Add Non-Fiction Book");
            Console.WriteLine("4. Compare Book Prices");
            Console.WriteLine("5. Apply Discount");
            Console.WriteLine("6. Copy a Book");
            Console.WriteLine("7. Show Total Books");
            Console.WriteLine("8. Exit");
            Console.Write("\nChoice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    store.DisplayAllBooks();
                    break;
                case "2":
                    AddFictionBook(store);
                    break;
                case "3":
                    AddNonFictionBook(store);
                    break;
                case "4":
                    store.CompareBooks();
                    break;
                case "5":
                    store.ApplyDiscount();
                    break;
                case "6":
                    store.CopyBook();
                    break;
                case "7":
                    Book.ShowTotal();
                    break;
                case "8":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static void AddFictionBook(Bookstore store)
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Author: ");
        string author = Console.ReadLine();
        Console.Write("Price: ");
        if (double.TryParse(Console.ReadLine(), out double price))
        {
            Console.Write("Genre: ");
            string genre = Console.ReadLine();
            store.AddBook(new FictionBook(title, author, price, genre));
        }
        else
            Console.WriteLine("Invalid price!");
    }

    static void AddNonFictionBook(Bookstore store)
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Author: ");
        string author = Console.ReadLine();
        Console.Write("Price: ");
        if (double.TryParse(Console.ReadLine(), out double price))
        {
            Console.Write("Category: ");
            string category = Console.ReadLine();
            store.AddBook(new NonFictionBook(title, author, price, category));
        }
        else
            Console.WriteLine("Invalid price!");
    }
}