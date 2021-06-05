using System.Windows;
using WPF_Exam.DapperDll;
using WPF_Exam.ViewModel;

namespace WPF_Exam.View.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Admin currentAdmin;
        public AdminWindow()
        {
            InitializeComponent();
        }

        public AdminWindow(Admin currentAdmin)
        {
            InitializeComponent();
            this.currentAdmin = currentAdmin;
            this.LoginLabel.Content = $"Login: {this.currentAdmin.Admin_Login}";
            this.DataContext = new AdminViewModel(this);
        }
    }
}
