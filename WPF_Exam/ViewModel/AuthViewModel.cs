using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WPF_Exam.Model;
using WPF_Exam.View.ViewModel;

namespace WPF_Exam.ViewModel
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private AuthWindow authWindow;

        private bool isAdmin = true;

        public bool IsAdmin
        { 
            get { return isAdmin; }
            set { isAdmin = value; OnPropertyChanged("IsAdmin"); }
        }


        private string loginStr;
        public string LoginStr
        {
            get { return loginStr; }
            set { loginStr = value; OnPropertyChanged("LoginStr"); }
        }

        private string passStr;
        public string PassStr
        {
            get { return passStr; }
            set { passStr = value; OnPropertyChanged("PassStr"); }
        }


        private RelayCommand loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand(obj =>
                {
                    if (Logic.Auth(loginStr, passStr, isAdmin))
                        this.authWindow.Close();
                    else
                        MessageBox.Show("Инкорректные данные.");
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public AuthViewModel(AuthWindow authWindow) => this.authWindow = authWindow;
    }
}
