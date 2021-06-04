using System;
using System.Windows;
using System.Windows.Media;

namespace WPF_Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            InitializeComponent();

            string themeName = "AuthTheme";

            var uri = new Uri(@"View/Authentication/" + themeName + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
