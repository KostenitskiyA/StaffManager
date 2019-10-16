using System.Windows;
using StaffManager.Model;
using StaffManager.ViewModel;

namespace StaffManager.View
{
    public partial class CalculateSalaryView : Window
    {
        public CalculateSalaryView()
        {
            InitializeComponent();
            CalculateSalaryViewModel viewModel = new CalculateSalaryViewModel();
            this.DataContext = viewModel;
        }

        public CalculateSalaryView(User user)
        {
            InitializeComponent();
            CalculateSalaryViewModel viewModel = new CalculateSalaryViewModel(user);
            this.DataContext = viewModel;
        }
    }
}
