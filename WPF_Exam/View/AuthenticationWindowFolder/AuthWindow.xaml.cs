using System;
using System.Windows;
using System.Windows.Media;
using WPF_Exam.View.ViewModel;
using WPF_Exam.ViewModel;

namespace WPF_Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            this.DataContext = new AuthViewModel(this);

            this.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            InitializeComponent();

            string themeName = "AuthTheme";

            var uri = new Uri(@"View/AuthenticationWindowFolder/" + themeName + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
