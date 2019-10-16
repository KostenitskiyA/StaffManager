using System;
using System.Windows;
using StaffManager.Model;
using StaffManager.ViewModel;

namespace StaffManager.View
{
    public partial class MainView : Window
    {
        public MainView(User user)
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel() { Title = user.Name, User = user };
            this.DataContext = viewModel;

            if (viewModel.Close == null)
            {
                viewModel.Close = new Action(this.Close);
            }
        }
    }
}
