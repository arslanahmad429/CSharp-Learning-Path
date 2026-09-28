using System;
using Microsoft.Data.SqlClient;

namespace LibraryData
{
    public class LibraryService
    {
        private string connStr;

        public LibraryService(string c)
        {
            connStr = c;
        }

        public bool IssueBook(int bId, int mId)
        {
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlTransaction t = con.BeginTransaction();

            string q1 = "SELECT COUNT(*) FROM Members WHERE MemberId = @mId";
            SqlCommand cmd1 = new SqlCommand(q1, con, t);
            cmd1.Parameters.AddWithValue("@mId", mId);

            string q2 = "SELECT Stock FROM Books WHERE BookId = @bId";
            SqlCommand cmd2 = new SqlCommand(q2, con, t);
            cmd2.Parameters.AddWithValue("@bId", bId);

            string q3 = "INSERT INTO IssuedBooks (BookId, MemberId, IssueDate, ReturnDate) VALUES (@bId, @mId, @d, NULL)";
            SqlCommand cmd3 = new SqlCommand(q3, con, t);
            cmd3.Parameters.AddWithValue("@bId", bId);
            cmd3.Parameters.AddWithValue("@mId", mId);
            cmd3.Parameters.AddWithValue("@d", DateTime.Now);

            string q4 = "UPDATE Books SET Stock = Stock - 1 WHERE BookId = @bId";
            SqlCommand cmd4 = new SqlCommand(q4, con, t);
            cmd4.Parameters.AddWithValue("@bId", bId);

            try
            {
                int count = (int)cmd1.ExecuteScalar();
                if (count == 0) throw new Exception("no member");

                object stock = cmd2.ExecuteScalar();
                if (stock == null || Convert.ToInt32(stock) <= 0) throw new Exception("no stock");

                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();

                t.Commit();
                con.Close();
            }
            catch (Exception e)
            {
                t.Rollback();
                con.Close();
                return false;
            }

            return true;
        }

        public bool ReturnBook(int issueId)
        {
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlTransaction t = con.BeginTransaction();

            string q1 = "SELECT BookId FROM IssuedBooks WHERE IssueId = @iId AND ReturnDate IS NULL";
            SqlCommand cmd1 = new SqlCommand(q1, con, t);
            cmd1.Parameters.AddWithValue("@iId", issueId);

            string q2 = "UPDATE IssuedBooks SET ReturnDate = @d WHERE IssueId = @iId AND ReturnDate IS NULL";
            SqlCommand cmd2 = new SqlCommand(q2, con, t);
            cmd2.Parameters.AddWithValue("@d", DateTime.Now);
            cmd2.Parameters.AddWithValue("@iId", issueId);

            string q3 = "UPDATE Books SET Stock = Stock + 1 WHERE BookId = @bId";
            SqlCommand cmd3 = new SqlCommand(q3, con, t);

            try
            {
                object bObj = cmd1.ExecuteScalar();
                if (bObj == null) throw new Exception("no record");

                int bId = Convert.ToInt32(bObj);
                cmd3.Parameters.AddWithValue("@bId", bId);

                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();

                t.Commit();
                con.Close();
            }
            catch (Exception e)
            {
                t.Rollback();
                con.Close();
                return false;
            }

            return true;
        }
    }
}
