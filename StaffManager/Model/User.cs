using System;

namespace StaffManager.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; } 
        public string Password { get; set; }
        public string Name { get; set; }
        public string WorkGroup { get; set; }
        public int SeniorID { get; set; }
        public double Salary { get; set; }
        public DateTime StartDate { get; set; }

        public User() { }

        public User(string login, string password, string name, string workGroup, int seniorId, double salary, DateTime startDate)
        {
            Login = login;
            Password = password;
            Name = name;
            WorkGroup = workGroup;
            SeniorID = seniorId;
            Salary = salary;
            StartDate = startDate;
        }

        public User(int id, string login, string password, string name, string workGroup, int seniorId, double salary, DateTime startDate)
        {
            ID = id;
            Login = login;
            Password = password;
            Name = name;
            WorkGroup = workGroup;
            SeniorID = seniorId;
            Salary = salary;
            StartDate = startDate;
        }
    }
}
