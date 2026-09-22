using System.Collections.Generic;
using System.IO;

namespace BookRecordsApp
{
    public class BookDataAccess
    {
        private string filePath = "books.txt";
        private string backupPath = "books_backup.txt";

        public void AddBook(Book book)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(book.ToRecord());
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> list = new List<Book>();

            if (!File.Exists(filePath))
                return list;

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Book b = ParseRecord(line);
                    if (b != null)
                    {
                        list.Add(b);
                    }
                }
            }

            return list;
        }

        public Book FindBookById(int id)
        {
            List<Book> books = GetAllBooks();

            foreach (var b in books)
            {
                if (b.Id == id)
                    return b;
            }

            return null;
        }

        public bool CreateBackup()
        {
            if (!File.Exists(filePath))
                return false;

            using (FileStream src = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (FileStream dest = new FileStream(backupPath, FileMode.Create, FileAccess.Write))
            {
                int b;
                while ((b = src.ReadByte()) != -1)
                {
                    dest.WriteByte((byte)b);
                }
            }

            return true;
        }

        private Book ParseRecord(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            string[] data = line.Split(',');
            if (data.Length != 4)
                return null;

            int id;
            double price;

            if (!int.TryParse(data[0].Trim(), out id) || !double.TryParse(data[3].Trim(), out price))
            {
                return null;
            }

            return new Book(id, data[1].Trim(), data[2].Trim(), price);
        }
    }
}