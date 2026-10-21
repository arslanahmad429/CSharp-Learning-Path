using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CampusEventManagement.Models;

namespace CampusEventManagement.Repositories
{
    public class EventRepository
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EventDB;Integrated Security=True;TrustServerCertificate=True;";

        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();

            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT Id, Title, Category, Date, Venue FROM Events";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Event item = new Event();
                item.Id = Convert.ToInt32(reader["Id"]);
                item.Title = reader["Title"].ToString();
                item.Category = reader["Category"].ToString();
                item.Date = Convert.ToDateTime(reader["Date"]);
                item.Venue = reader["Venue"].ToString();

                events.Add(item);
            }

            con.Close();

            return events;
        }

        public void AddEvent(Event ev)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "INSERT INTO Events (Title, Category, Date, Venue) VALUES (@Title, @Category, @Date, @Venue)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Title", ev.Title);
            cmd.Parameters.AddWithValue("@Category", ev.Category);
            cmd.Parameters.AddWithValue("@Date", ev.Date);
            cmd.Parameters.AddWithValue("@Venue", ev.Venue);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
