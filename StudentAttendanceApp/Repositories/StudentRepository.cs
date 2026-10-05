using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using StudentAttendanceApp.Models;

namespace StudentAttendanceApp.Repositories
{
    public class StudentRepository
    {
        private string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AttendanceDB;Integrated Security=True;";

        public async Task<List<Student>> GetAllStudents()
        {
            List<Student> list = new List<Student>();
            SqlConnection con = new SqlConnection(connStr);
            string q = "SELECT * FROM Students";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            await Task.Run(() => da.Fill(dt));

            foreach (DataRow r in dt.Rows)
            {
                Student s = new Student();
                s.StudentId = Convert.ToInt32(r["StudentId"]);
                s.StudentName = r["StudentName"].ToString();
                s.RegistrationNumber = r["RegistrationNumber"].ToString();
                s.Department = r["Department"].ToString();
                s.AttendancePercentage = Convert.ToDecimal(r["AttendancePercentage"]);
                list.Add(s);
            }

            return list;
        }

        public async Task<Student> GetStudentById(int id)
        {
            Student s = new Student();
            SqlConnection con = new SqlConnection(connStr);
            string q = "SELECT * FROM Students WHERE StudentId = @id";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            await Task.Run(() => da.Fill(dt));

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                s.StudentId = Convert.ToInt32(r["StudentId"]);
                s.StudentName = r["StudentName"].ToString();
                s.RegistrationNumber = r["RegistrationNumber"].ToString();
                s.Department = r["Department"].ToString();
                s.AttendancePercentage = Convert.ToDecimal(r["AttendancePercentage"]);
            }

            return s;
        }

        public async Task AddStudent(Student s)
        {
            SqlConnection con = new SqlConnection(connStr);
            string q = "INSERT INTO Students (StudentName, RegistrationNumber, Department, AttendancePercentage) VALUES (@n, @r, @d, @a)";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@n", s.StudentName);
            cmd.Parameters.AddWithValue("@r", s.RegistrationNumber);
            cmd.Parameters.AddWithValue("@d", s.Department);
            cmd.Parameters.AddWithValue("@a", s.AttendancePercentage);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            con.Close();
        }

        public async Task UpdateStudent(Student s)
        {
            SqlConnection con = new SqlConnection(connStr);
            string q = "UPDATE Students SET StudentName = @n, RegistrationNumber = @r, Department = @d, AttendancePercentage = @a WHERE StudentId = @id";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@n", s.StudentName);
            cmd.Parameters.AddWithValue("@r", s.RegistrationNumber);
            cmd.Parameters.AddWithValue("@d", s.Department);
            cmd.Parameters.AddWithValue("@a", s.AttendancePercentage);
            cmd.Parameters.AddWithValue("@id", s.StudentId);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            con.Close();
        }

        public async Task DeleteStudent(int id)
        {
            SqlConnection con = new SqlConnection(connStr);
            string q = "DELETE FROM Students WHERE StudentId = @id";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            con.Close();
        }
    }
}
