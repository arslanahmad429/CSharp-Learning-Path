using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CampusEventManagement.Models;

namespace CampusEventManagement.Repositories
{
    public class EventRepository
    {
        string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EventDB;Integrated Security=True;";

        public List<Event> GetAllEvents()
        {
            List<Event> list = new List<Event>();
            SqlConnection con = new SqlConnection(connStr);
            string q = "SELECT * FROM Events";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                Event ev = new Event();
                ev.Id = Convert.ToInt32(r["Id"]);
                ev.Title = r["Title"].ToString();
                ev.Category = r["Category"].ToString();
                ev.Date = Convert.ToDateTime(r["Date"]);
                ev.Venue = r["Venue"].ToString();
                list.Add(ev);
            }
            con.Close();
            return list;
        }

        public void AddEvent(Event ev)
        {
            SqlConnection con = new SqlConnection(connStr);
            string q = "INSERT INTO Events (Title, Category, Date, Venue) VALUES (@t, @c, @d, @v)";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@t", ev.Title);
            cmd.Parameters.AddWithValue("@c", ev.Category);
            cmd.Parameters.AddWithValue("@d", ev.Date);
            cmd.Parameters.AddWithValue("@v", ev.Venue);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
