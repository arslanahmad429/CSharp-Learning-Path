using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace LibraryData
{
    public class MemberRepository
    {
        private string connStr;

        public MemberRepository(string c)
        {
            connStr = c;
        }

        public int AddMember(Member m)
        {
            string q = "INSERT INTO Members (Name) VALUES (@n)";
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@n", m.Name);
            
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            
            if (res > 0) return res;
            return 0;
        }

        public List<Member> GetAllMembers()
        {
            List<Member> list = new List<Member>();
            string q = "SELECT * FROM Members";
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(q, con);
            
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            
            while (r.Read())
            {
                Member m = new Member();
                m.MemberId = (int)r["MemberId"];
                m.Name = r["Name"].ToString();
                list.Add(m);
            }
            con.Close();
            
            return list;
        }
    }
}
