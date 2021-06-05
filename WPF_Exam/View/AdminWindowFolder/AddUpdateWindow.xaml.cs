using System.Windows;
using WPF_Exam.ViewModel;

namespace WPF_Exam.View.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateWindow.xaml
    /// </summary>
    public partial class AddUpdateWindow : Window
    {
        public int workLevel;
        public int foreign_Id;
        public object currentItem;

        public AddUpdateWindow(int workLevel, int foreign_Id)
        {
            this.workLevel = workLevel;
            this.foreign_Id = foreign_Id;
            InitializeComponent();
            this.DataContext = new AddUpdateViewModel(this);
            this.MainLabel.Content = "Добавить";

            if (this.workLevel == 3)
            {
                this.InfoLabel.IsEnabled = true;
                this.YesRadio.IsEnabled = true;
                this.NoRadio.IsEnabled = true;
            }
        }

        public AddUpdateWindow(int workLevel, object currentItem)
        {
            this.workLevel = workLevel;
            this.currentItem = currentItem;
            InitializeComponent();
            this.DataContext = new AddUpdateViewModel(this);
            this.MainLabel.Content = "Обновить";

            if (this.workLevel == 3)
            {
                this.InfoLabel.IsEnabled = true;
                this.YesRadio.IsEnabled = true;
                this.NoRadio.IsEnabled = true;
            }
        }
    }
}
