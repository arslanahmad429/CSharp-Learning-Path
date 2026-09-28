using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace LibraryData
{
    public class BookRepository
    {
        private string connStr;

        public BookRepository(string c)
        {
            connStr = c;
        }

        public int AddBook(Book b)
        {
            string q = "INSERT INTO Books (Title, Author, Stock) VALUES (@t, @a, @s)";
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(q, con);

            cmd.Parameters.AddWithValue("@t", b.Title);
            cmd.Parameters.AddWithValue("@a", b.Author);
            cmd.Parameters.AddWithValue("@s", b.Stock);

            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();

            if (res > 0) return res;
            return 0;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> list = new List<Book>();
            string q = "SELECT * FROM Books";
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(q, con);
            
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            
            while (r.Read())
            {
                Book b = new Book();
                b.BookId = Convert.ToInt32(r["BookId"]);
                b.Title = r["Title"].ToString();
                b.Author = r["Author"].ToString();
                b.Stock = Convert.ToInt32(r["Stock"]);
                list.Add(b);
            }
            con.Close();
            return list;
        }

        public int UpdateBookStock(int id, int s)
        {
            string q = "Update Books SET Stock = @s where bookId = @id";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();

            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@s", s);
            cmd.Parameters.AddWithValue("@id", id);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            
            if (res > 0) return res;
            return 0;
        }

        public int DeleteBook(int id)
        {
            string q = "DELETE FROM Books WHERE BookId = @id";
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            
            if (res > 0) return res;
            return 0;
        }
    }
}
