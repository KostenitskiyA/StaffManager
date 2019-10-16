using System;
using System.Windows.Input;
using StaffManager.Infrastructure;
using StaffManager.Model;
using StaffManager.View;


namespace StaffManager.ViewModel
{
    public class LogInViewModel : ViewModel
    {
        private MainModel model = new MainModel();

        public string Title { get; set; } = "LogIn";
        public Action Close { get; set; }

        public string SelectedLogin { get; set; }
        public string SelectedPassword { get; set; }

        // Команда входа
        #region LogInCommand 
        RelayCommand _logInCommand;
        public ICommand LogInCommand
        {
            get
            {
                if (_logInCommand == null) { _logInCommand = new RelayCommand(ExecuteLogInCommand, CanExecuteLogInCommand); }
                return _logInCommand;
            }
        }
        public void ExecuteLogInCommand(object parameter)
        {
            User user = model.GetUser(SelectedLogin, SelectedPassword);

            if (user != null)
            {
                MainView mainView = new MainView(user);
                mainView.Show();
                Close();
            }
        }
        public bool CanExecuteLogInCommand(object parameter)
        {
            if (SelectedLogin != null && SelectedPassword != null && SelectedLogin != "" && SelectedPassword != "")
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
