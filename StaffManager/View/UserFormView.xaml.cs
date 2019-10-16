using System;
using System.Windows;
using StaffManager.Model;
using StaffManager.ViewModel;

namespace StaffManager.View
{
    public partial class UserFormView : Window
    {
        MainModel model = new MainModel();

        public UserFormView()
        {
            InitializeComponent();
            AddUserViewModel addUserViewModel = new AddUserViewModel() { Seniors = model.GetSeniors() };
            this.DataContext = addUserViewModel;

            if (addUserViewModel.Close == null)
            {
                addUserViewModel.Close = new Action(this.Close);
            }
        }

        public UserFormView(User user)
        {
            InitializeComponent();
            EditUserViewModel editUserViewModel = new EditUserViewModel() { Title = user.Name, User = user, Seniors = model.GetSeniors(), SelectedWorkGroup = user.WorkGroup, SelectedSenior = model.GetUser(user.SeniorID) };
            this.DataContext = editUserViewModel;

            if (editUserViewModel.Close == null)
            {
                editUserViewModel.Close = new Action(this.Close);
            }
        }
    }
}
