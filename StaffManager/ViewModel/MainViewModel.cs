using System;
using System.Collections.Generic;
using System.Windows.Input;

using StaffManager.Infrastructure;
using StaffManager.Model;
using StaffManager.View;

namespace StaffManager.ViewModel    
{
    public class MainViewModel : ViewModel
    {
        private MainModel model = new MainModel();

        public string Title { get; set; } = "Main";
        public Action Close { get; set; }
        
        public User User { get; set; }
        public List<User> Subordinates { get; set; }
        public User SelectedUser { get; set; }

        // Обновление таблицы
        #region UpdateCommand 
        RelayCommand _updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null) { _updateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand); }
                return _updateCommand;
            }
        }
        public void ExecuteUpdateCommand(object parameter)
        {
            if (User.ID != 0) // Если не администратор
            {
                Subordinates = model.GetAllSubordinates(User.ID);
            }
            else
            {
                Subordinates = model.GetAllEmployees();
            }

            OnPropertyChanged("Subordinates");
        }
        public bool CanExecuteUpdateCommand(object parameter)
        {
            return true;
        }
        #endregion

        // Вызов окна добавления пользователя
        #region AddCommand 
        RelayCommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null) { _addCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand); }
                return _addCommand;
            }
        }
        public void ExecuteAddCommand(object parameter)
        {
            UserFormView userFormView = new UserFormView();
            userFormView.Show();
        }
        public bool CanExecuteAddCommand(object parameter)
        {
            if (User.WorkGroup != "Employee")
            {
                return true;
            }

            return false;
        }
        #endregion

        // Вызов окна редактирования пользователя
        #region EditCommand 
        RelayCommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null) { _editCommand = new RelayCommand(ExecuteEditCommand, CanExecuteEditCommand); }
                return _editCommand;
            }
        }
        public void ExecuteEditCommand(object parameter)
        {
            UserFormView userFormView = new UserFormView(SelectedUser);
            userFormView.Show();
        }
        public bool CanExecuteEditCommand(object parameter)
        {
            if (SelectedUser != null)
            {
                return true;
            }

            return false;
        }
        #endregion

        // Удаление пользователя
        #region RemoveCommand 
        RelayCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null) { _removeCommand = new RelayCommand(ExecuteRemoveCommand, CanExecuteRemoveCommand); }
                return _removeCommand;
            }
        }
        public void ExecuteRemoveCommand(object parameter)
        {
            model.RemoveUser(SelectedUser.ID);
        }
        public bool CanExecuteRemoveCommand(object parameter)
        {
            if (SelectedUser != null)
            {
                return true;
            }

            return false;
        }
        #endregion

        // Вызов окна расчёта зарплаты пользователя
        #region SalaryCommand 
        RelayCommand _salaryCommand;
        public ICommand SalaryCommand
        {
            get
            {
                if (_salaryCommand == null) { _salaryCommand = new RelayCommand(ExecuteSalaryCommand, CanExecuteSalaryCommand); }
                return _salaryCommand;
            }
        }
        public void ExecuteSalaryCommand(object parameter)
        {
            CalculateSalaryView calculateSalaryView = new CalculateSalaryView(SelectedUser);
            calculateSalaryView.Show();
        }
        public bool CanExecuteSalaryCommand(object parameter)
        {
            if (SelectedUser != null)
            {
                return true;
            }

            return false;
        }
        #endregion

        // Вызов окна расчёта зарплат пользователей
        #region AllSalariesCommand 
        RelayCommand _allSalariesCommand;
        public ICommand AllSalariesCommand
        {
            get
            {
                if (_allSalariesCommand == null) { _allSalariesCommand = new RelayCommand(ExecuteAllSalariesCommand, CanExecuteAllSalariesCommand); }
                return _allSalariesCommand;
            }
        }
        public void ExecuteAllSalariesCommand(object parameter)
        {
            CalculateSalaryView calculateAllSalariesView = new CalculateSalaryView();
            calculateAllSalariesView.Show();
        }
        public bool CanExecuteAllSalariesCommand(object parameter)
        {
            if (User.ID == 0) // Только для администратора 
            {
                return true;
            }

            return false;
        }
        #endregion

        // Выход
        #region LogOutCommand 
        RelayCommand _logOutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                if (_logOutCommand == null) { _logOutCommand = new RelayCommand(ExecuteLogOutCommand, CanExecuteLogOutCommand); }
                return _logOutCommand;
            }
        }
        public void ExecuteLogOutCommand(object parameter)
        {
            LogInView logInView = new LogInView();
            logInView.Show();

            Close();
        }
        public bool CanExecuteLogOutCommand(object parameter)
        {
            if (User != null)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
