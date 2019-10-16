using System;
using System.Windows.Input;
using StaffManager.Infrastructure;
using StaffManager.Model;

namespace StaffManager.ViewModel
{
    public class CalculateSalaryViewModel : ViewModel
    {
        MainModel model = new MainModel(); // Модель
        public string Title { get; set; } = "CalculateSalary";

        public string UserName { get; set; }
        public User SelectedUser { get; set; } = null;
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public double Salary { get; set; } = 0;

        public CalculateSalaryViewModel() 
        {
            Title = "Расчёт зарплат пользователей";
            UserName = "Все пользователи";
        }

        public CalculateSalaryViewModel(User selectedUser)
        {
            Title = selectedUser.Name;
            UserName = selectedUser.Name;
            SelectedUser = selectedUser;
        }

        #region CalculateSalaryCommand 
        RelayCommand _calculateSalaryCommand;
        public ICommand CalculateSalaryCommand
        {
            get
            {
                if (_calculateSalaryCommand == null) { _calculateSalaryCommand = new RelayCommand(ExecuteCalculateSalaryCommand, CanExecuteCalculateSalaryCommand); }
                return _calculateSalaryCommand;
            }
        }
        public void ExecuteCalculateSalaryCommand(object parameter)
        {
            if (SelectedUser != null)
            {
                Salary = model.CalculateSalary(SelectedUser, SelectedDate);
            }
            else
            {
                Salary = model.CalculateAllSalaries(SelectedDate);
            }

            OnPropertyChanged("Salary");
        }
        public bool CanExecuteCalculateSalaryCommand(object parameter)
        {
            return true;
        }
        #endregion
    }
}
