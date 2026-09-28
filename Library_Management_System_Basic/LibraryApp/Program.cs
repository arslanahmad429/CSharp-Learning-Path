using System;
using System.Collections.Generic;
using LibraryData;

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string db = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;";

            BookRepository br = new BookRepository(db);
            MemberRepository mr = new MemberRepository(db);
            LibraryService ls = new LibraryService(db);
            BackupHelper bh = new BackupHelper();

            while (true)
            {
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Add Member");
                Console.WriteLine("4. View All Members");
                Console.WriteLine("5. Issue Book");
                Console.WriteLine("6. Return Book");
                Console.WriteLine("7. Export Books JSON");
                Console.WriteLine("8. Import Books JSON");
                Console.WriteLine("9. Update Stock");
                Console.WriteLine("10. Delete Book");
                Console.WriteLine("11. Exit");
                Console.Write("Choice: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Title: ");
                    string t = Console.ReadLine();
                    Console.Write("Author: ");
                    string a = Console.ReadLine();
                    Console.Write("Stock: ");
                    int s = Convert.ToInt32(Console.ReadLine());

                    Book b = new Book();
                    b.Title = t;
                    b.Author = a;
                    b.Stock = s;

                    br.AddBook(b);
                    Console.WriteLine("Done.");
                }
                else if (choice == "2")
                {
                    List<Book> list = br.GetAllBooks();
                    foreach (Book b in list)
                    {
                        Console.WriteLine(b.BookId + " - " + b.Title + " - " + b.Author + " - " + b.Stock);
                    }
                }
                else if (choice == "3")
                {
                    Console.Write("Member Name: ");
                    string n = Console.ReadLine();

                    Member m = new Member();
                    m.Name = n;
                    
                    mr.AddMember(m);
                    Console.WriteLine("Done.");
                }
                else if (choice == "4")
                {
                    List<Member> list = mr.GetAllMembers();
                    foreach (Member m in list)
                    {
                        Console.WriteLine(m.MemberId + " - " + m.Name);
                    }
                }
                else if (choice == "5")
                {
                    Console.Write("Book ID: ");
                    int bId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Member ID: ");
                    int mId = Convert.ToInt32(Console.ReadLine());

                    bool ok = ls.IssueBook(bId, mId);
                    if (ok) Console.WriteLine("Issued.");
                    else Console.WriteLine("Error.");
                }
                else if (choice == "6")
                {
                    Console.Write("Book ID: ");
                    int bId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Member ID: ");
                    int mId = Convert.ToInt32(Console.ReadLine());

                    bool ok = ls.ReturnBook(bId, mId);
                    if (ok) Console.WriteLine("Returned.");
                    else Console.WriteLine("Error.");
                }
                else if (choice == "7")
                {
                    Console.Write("File path: ");
                    string p = Console.ReadLine();
                    List<Book> list = br.GetAllBooks();
                    bh.BackupBooksToJson(list, p);
                    Console.WriteLine("Done.");
                }
                else if (choice == "8")
                {
                    Console.Write("File path: ");
                    string p = Console.ReadLine();
                    List<Book> list = bh.LoadBooksFromJsonBackup(p);
                    foreach (Book b in list)
                    {
                        Console.WriteLine(b.Title + " " + b.Stock);
                    }
                }
                else if (choice == "9")
                {
                    Console.Write("Book ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("New Stock: ");
                    int s = Convert.ToInt32(Console.ReadLine());

                    br.UpdateBookStock(id, s);
                    Console.WriteLine("Done.");
                }
                else if (choice == "10")
                {
                    Console.Write("Book ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    br.DeleteBook(id);
                    Console.WriteLine("Done.");
                }
                else if (choice == "11")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong choice.");
                }
                Console.WriteLine();
            }
        }
    }
}
