using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace StaffManager.Model
{
    public class MainModel
    {
        private DataContext db = new DataContext();

        public User GetUser(string login, string password)
        {
            try
            {
                var query = db.Users.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));

                if (query != null)
                {
                    return query;
                }
                else
                {
                    throw new Exception("Пользователь не найден");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка");
            }

            return null;
        }

        public User GetUser(int id)
        {
            try
            {
                User query = db.Users.Find(id);

                if (query != null)
                {
                    return query;
                }
                else
                {
                    throw new Exception("Пользователь не найден");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка");
            }

            return null;
        }
        
        public List<User> GetAllEmployees()
        {
            List<User> subordinates = new List<User>();

            var query = db.Users.Where(u => u.ID != 0).Select(u => u);

            foreach (User user in query)
            {
                subordinates.Add(user);
            }

            return subordinates;
        }

        public List<User> GetAllSubordinates(int id)
        {
            List<User> subordinates = new List<User>();

            var query = db.Users.Where(u => u.SeniorID == id).Select(u => u);

            foreach (User user in query)
            {
                subordinates.Add(user);
                subordinates.AddRange(GetAllSubordinates(user.ID));
            }

            return subordinates;
        }

        public List<User> GetSubordinates(int id)
        {
            List<User> subordinates = new List<User>();

            var query = db.Users.Where(u => u.SeniorID == id).Select(u => u);

            foreach (User user in query)
            {
                subordinates.Add(user);
            }

            return subordinates;
        }

        public List<User> GetSeniors()
        {
            List<User> subordinates = new List<User>();

            var query = db.Users.Where(u => u.WorkGroup != "Employee").Select(u => u);

            foreach (User user in query)
            {
                subordinates.Add(user);
            }

            return subordinates;
        }



        public void AddUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Пользователь добавлен");
            }
        }

        public void EditUser(int id, User user)
        {
            try
            {
                var changes = db.Users.Find(id);

                changes.ID = user.ID;
                changes.Login = user.Login;
                changes.Password = user.Password;
                changes.Name = user.Name;
                changes.WorkGroup = user.WorkGroup;
                changes.SeniorID = user.SeniorID;
                changes.Salary = user.Salary;
                changes.StartDate = user.StartDate;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Пользователь изменён");
            }
        }

        public void RemoveUser(int id)
        {
            try
            {
                var changes = db.Users.Find(id);
                db.Users.Remove(changes);
                db.SaveChanges();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                MessageBox.Show("Пользователь удалён");
            }
        }



        public double CalculateSalary(User user, DateTime selectedDate)
        {
            double result = 0;

            result += Calculate(user.ID, user.WorkGroup, user.Salary, user.StartDate, selectedDate);

            return result;
        }

        public double CalculateAllSalaries(DateTime selectedDate)
        {
            double result = 0;

            List<User> subordinates = GetAllEmployees();

            foreach (User user in subordinates)
            {
                result += Calculate(user.ID, user.WorkGroup, user.Salary, user.StartDate, selectedDate);
            }

            return result;
        }
        


        public double Calculate(int id, string workGroup, double salary, DateTime startDate, DateTime selectedDate)
        {
            double result = salary * Allowance(workGroup, startDate, selectedDate);

            switch (workGroup)
            {
                case "Manager":
                    result += SubordinateCalculate(id, selectedDate) * 0.005d;
                    break;
                case "Salesman":
                    result += SubordinateCalculate(id, selectedDate) * 0.003d;
                    break;
            }

            return result;
        }

        public double Allowance(string workGroup, DateTime startDate, DateTime selectedDate)
        {
            double allowance = Math.Pow(MultiplierCalculate(workGroup), TimeCalculate(startDate, selectedDate));

            switch (workGroup)
            {
                case "Manager":
                    if (allowance < 1.4d) { return allowance; }
                    else { return 1.4d; }
                case "Salesman":
                    if (allowance < 1.35d) { return allowance; }
                    else { return 1.35d; }
                case "Employee":
                    if (allowance < 1.3d) { return allowance; }
                    else { return 1.3d; }
                default:
                    return 0;
            }
        }

        public double MultiplierCalculate(string workGroup)
        {
            switch (workGroup)
            {               
                case "Manager":
                    return 1.05d;
                case "Salesman":
                    return 1.01d;
                case "Employee":
                    return 1.03d;
                default:
                    return 0;
            }
        }

        public int TimeCalculate(DateTime startDate, DateTime selectedDate)
        {
            int deltaTime = Convert.ToInt32(selectedDate.Year - startDate.Year);

            if (deltaTime > 0)
            {
                return deltaTime;
            }
            else
            {
                return 0;
            }
        }

        public double SubordinateCalculate(int id, DateTime selectedDate)
        {
            double salary = 0;

            List<User> subordinates = GetSubordinates(id);

            foreach (User user in subordinates)
            {
                salary += Calculate(user.ID, user.WorkGroup, user.Salary, user.StartDate, selectedDate);
            }

            return salary;
        }
    }
}
