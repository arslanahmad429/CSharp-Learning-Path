using System;
using System.Collections.Generic;

namespace BookRecordsApp
{
    class Program
    {
        static BookDataAccess db = new BookDataAccess();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Book Records Menu ===");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Search Book by ID");
                Console.WriteLine("4. Backup File");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        ShowAllBooks();
                        break;
                    case "3":
                        SearchBook();
                        break;
                    case "4":
                        MakeBackup();
                        break;
                    case "5":
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Enter ID: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID entered.");
                return;
            }

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            Console.Write("Enter Price: ");
            double price;
            if (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid price entered.");
                return;
            }

            if (title != null) title = title.Replace(",", " ");
            if (author != null) author = author.Replace(",", " ");

            Book b = new Book(id, title, author, price);
            db.AddBook(b);

            Console.WriteLine("Book saved successfully!");
        }

        static void ShowAllBooks()
        {
            List<Book> list = db.GetAllBooks();

            if (list.Count == 0)
            {
                Console.WriteLine("No records found.");
                return;
            }

            foreach (var b in list)
            {
                b.DisplayInfo();
            }
        }

        static void SearchBook()
        {
            Console.Write("Enter Book ID to find: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Book b = db.FindBookById(id);
            if (b != null)
            {
                b.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        static void MakeBackup()
        {
            if (db.CreateBackup())
            {
                Console.WriteLine("Backup created successfully.");
            }
            else
            {
                Console.WriteLine("Original file does not exist yet.");
            }
        }
    }
}