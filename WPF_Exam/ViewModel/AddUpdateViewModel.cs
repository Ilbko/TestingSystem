using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_Exam.Model;
using WPF_Exam.View.AdminWindowFolder;
using WPF_Exam.View.ViewModel;

namespace WPF_Exam.ViewModel
{
    public class AddUpdateViewModel : INotifyPropertyChanged
    {
        private AddUpdateWindow addUpdateWindow;

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private bool isTrue = false;
        public bool IsTrue
        {
            get { return isTrue; }
            set { isTrue = value; OnPropertyChanged("IsTrue"); }
        }

        private RelayCommand clickCommand;
        public RelayCommand ClickCommand
        {
            get
            {
                return clickCommand ?? (new RelayCommand(obj => {
                    if (Name == string.Empty)
                        Name = "default";

                    if (this.addUpdateWindow.MainLabel.Content == "Добавить")
                        Logic.AddEntry(this.addUpdateWindow.workLevel, this.addUpdateWindow.foreign_Id, this.Name, this.IsTrue);

                    this.addUpdateWindow.Close();
                }));
            }
        }

        public AddUpdateViewModel(AddUpdateWindow addUpdateWindow) => this.addUpdateWindow = addUpdateWindow;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
