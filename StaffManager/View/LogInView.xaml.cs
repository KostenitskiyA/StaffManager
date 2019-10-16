using System;
using System.Windows;
using StaffManager.ViewModel;

namespace StaffManager.View
{
    public partial class LogInView : Window
    {
        public LogInView()
        {
            InitializeComponent();
            LogInViewModel viewModel = new LogInViewModel();
            this.DataContext = viewModel;
            if (viewModel.Close == null)
            {
                viewModel.Close = new Action(this.Close);
            }
        }
    }
}
