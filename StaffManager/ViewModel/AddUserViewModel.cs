using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using StaffManager.Infrastructure;
using StaffManager.Model;

namespace StaffManager.ViewModel
{
    class AddUserViewModel : ViewModel
    {
        private MainModel model = new MainModel();

        public string Title { get; set; } = "UserForm";
        public string ButtonContent { get; set; }
        public Action Close { get; set; }

        public User User { get; set; } = new User();

        public List<string> WorkGroups { get; } = new List<string> { "Manager", "Salesman", "Employee" };
        public string SelectedWorkGroup { get; set; }

        public List<User> Seniors { get; set; }
        public User SelectedSenior { get; set; }
        

        public AddUserViewModel() 
        {
            Title = "Добавление пользователя";
            ButtonContent = "Add";
        }

        #region ChangeCommand 
        RelayCommand _changeCommand;
        public ICommand ChangeCommand
        {
            get
            {
                if (_changeCommand == null) { _changeCommand = new RelayCommand(ExecuteChangeCommand, CanExecuteChangeCommand); }
                return _changeCommand;
            }
        }
        public void ExecuteChangeCommand(object parameter)
        {
            User.WorkGroup = SelectedWorkGroup;
            User.SeniorID = SelectedSenior.ID;

            model.AddUser(User);
            Close();
        }
        public bool CanExecuteChangeCommand(object parameter)
        {
            if (User.Login != "" && User.Password != "" && User.Name != "" && SelectedWorkGroup != null && SelectedSenior != null) 
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
