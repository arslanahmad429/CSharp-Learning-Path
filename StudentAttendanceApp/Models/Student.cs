using System;

namespace StudentAttendanceApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string RegistrationNumber { get; set; }
        public string Department { get; set; }
        public decimal AttendancePercentage { get; set; }
    }
}
