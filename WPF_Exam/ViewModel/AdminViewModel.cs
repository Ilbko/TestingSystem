using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_Exam.DapperDll;
using WPF_Exam.View.AdminWindowFolder;
using System.Linq;
using WPF_Exam.View.ViewModel;
using WPF_Exam.Model;
using System.Windows;

namespace WPF_Exam.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private AdminWindow adminWindow;
        private int workLevel;

        public int WorkLevel
        {
            get { return workLevel; }
            set { workLevel = value; OnPropertyChanged("WorkLevel"); }
        }


        #region Категории
        public ObservableCollection<Category> Categories { get; set; }

        private Category currentCategory = null;
        public Category CurrentCategory
        {
            get { return currentCategory; }
            set 
            { 
                currentCategory = value; 
                OnPropertyChanged("CurrentCategory");
                if (currentCategory != null)
                {
                    CurrentTests = new ObservableCollection<Test>(tests.Where(x => x.Test_CategoryId == currentCategory.Category_Id));
                    this.adminWindow.TestRadio.IsEnabled = true;
                }
                else
                {
                    CurrentTests = null;
                    this.adminWindow.TestRadio.IsEnabled = false;
                    this.WorkLevel = 0;
                }
                OnPropertyChanged("CurrentTests");
            }
        }
        #endregion

        #region Тесты
        public List<Test> tests { get; set; }
        public ObservableCollection<Test> CurrentTests { get; set; }

        private Test currentTest = null;
        public Test CurrentTest
        {
            get { return currentTest; }
            set 
            { 
                currentTest = value; 
                OnPropertyChanged("CurrentTest");
                if (currentTest != null)
                {
                    CurrentQuestions = new ObservableCollection<Question>(questions.Where(x => x.Question_TestId == currentTest.Test_Id));
                    this.adminWindow.QuestionRadio.IsEnabled = true;
                }
                else
                {
                    CurrentQuestions = null;
                    this.adminWindow.QuestionRadio.IsEnabled = false;
                    this.WorkLevel = 0;
                }
                OnPropertyChanged("CurrentQuestions");
            }
        }

        #endregion

        #region Вопросы
        public List<Question> questions { get; set; }
        public ObservableCollection<Question> CurrentQuestions { get; set; }

        private Question currentQuestion = null;
        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set 
            { 
                currentQuestion = value;
                OnPropertyChanged("CurrentQuestion");
                if (currentQuestion != null)
                {
                    CurrentAnswers = new ObservableCollection<Answer>(answers.Where(x => x.Answer_QuestionId == currentQuestion.Question_Id));
                    this.adminWindow.AnswerRadio.IsEnabled = true;
                }
                else
                {
                    CurrentAnswers = null;
                    this.adminWindow.AnswerRadio.IsEnabled = false;
                    this.WorkLevel = 0;
                }
                OnPropertyChanged("CurrentAnswers");
            }
        }
        #endregion

        #region Ответы
        public List<Answer> answers { get; set; }
        public ObservableCollection<Answer> CurrentAnswers { get; set; }

        private Answer currentAnswer = null;
        public Answer CurrentAnswer
        {
            get { return currentAnswer; }
            set { currentAnswer = value; OnPropertyChanged("CurrentAnswer"); }
        }
        #endregion

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj => {

                    //Logic.Add(workLevel, 
                    //    currentCategory != null ? currentCategory.Category_Id : 0, 
                    //    currentTest != null ? currentTest.Test_Id : 0, 
                    //    currentQuestion != null ? currentQuestion.Question_Id : 0);

                    switch (workLevel)
                    {
                        case 3:
                            {
                                Logic.Add(workLevel, currentQuestion != null ? currentQuestion.Question_Id : 0);
                                
                                this.WorkLevel = 2;

                                UpdateCurrentAnswers();

                                break;
                            }
                        case 2:
                            {
                                Logic.Add(workLevel, currentTest != null ? currentTest.Test_Id : 0);

                                this.WorkLevel = 1;

                                UpdateCurrentAnswers();
                                UpdateCurrentQuestions();

                                break;
                            }
                        case 1:
                            {
                                Logic.Add(workLevel, currentCategory != null ? currentCategory.Category_Id : 0);

                                this.WorkLevel = 0;

                                UpdateCurrentAnswers();
                                UpdateCurrentQuestions();
                                UpdateCurrentTests();

                                break;
                            }
                        case 0:
                            {
                                Logic.Add(workLevel, 0);

                                UpdateCurrentAnswers();
                                UpdateCurrentQuestions();
                                UpdateCurrentTests();
                                UpdateCurrentCategories();

                                break;
                            }
                    }
                }));
            }
        }
        
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ?? (updateCommand = new RelayCommand(obj => 
                {
                    //Logic.Update(workLevel, currentCategory, currentTest, currentQuestion, currentAnswer);
                    switch (workLevel)
                    {
                        case 3:
                            {
                                Logic.Update(workLevel, currentAnswer);

                                this.WorkLevel = 2;

                                UpdateCurrentAnswers();

                                break;
                            }
                        case 2:
                            {
                                Logic.Update(workLevel, currentQuestion);

                                this.WorkLevel = 1;

                                UpdateCurrentAnswers();
                                UpdateCurrentQuestions();

                                break;
                            }
                        case 1:
                            {
                                Logic.Update(workLevel, currentTest);

                                this.WorkLevel = 0;

                                UpdateCurrentAnswers();
                                UpdateCurrentQuestions();
                                UpdateCurrentTests();

                                break;
                            }
                        case 0:
                            {
                                Logic.Update(workLevel, currentCategory);

                                UpdateCurrentAnswers();
                                UpdateCurrentQuestions();
                                UpdateCurrentTests();
                                UpdateCurrentCategories();

                                break;
                            }
                    }
                }));
            }
        }
        
        
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(obj =>
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент? Если у элемента есть зависимости," +
                    " то они тоже будут удалены.", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        switch (workLevel)
                        {
                            case 3:
                                {
                                    if (currentAnswer != null)
                                    {
                                        Logic.Delete(currentAnswer, answers);

                                        this.WorkLevel = 2;

                                        UpdateCurrentAnswers();
                                    }
                                    else
                                        MessageBox.Show("Выберите ответ, который нужно удалить.");

                                    break;
                                }
                            case 2:
                                {
                                    if (currentQuestion != null)
                                    {
                                        Logic.Delete(currentQuestion, answers, questions);

                                        this.WorkLevel = 1;

                                        UpdateCurrentAnswers();
                                        UpdateCurrentQuestions();
                                    }
                                    else
                                        MessageBox.Show("Выберите вопрос, который нужно удалить.");

                                    break;
                                }
                            case 1:
                                {
                                    if (currentTest != null)
                                    {
                                        Logic.Delete(currentTest, answers, questions, tests);

                                        this.WorkLevel = 0;

                                        UpdateCurrentAnswers();
                                        UpdateCurrentQuestions();
                                        UpdateCurrentTests();
                                    }
                                    else
                                        MessageBox.Show("Выберите тест, который нужно удалить.");

                                    break;
                                }
                            case 0:
                                {
                                    if (currentCategory != null)
                                    {
                                        Logic.Delete(currentCategory, answers, questions, tests, Categories);

                                        UpdateCurrentAnswers();
                                        UpdateCurrentQuestions();
                                        UpdateCurrentTests();
                                        UpdateCurrentCategories();
                                    }
                                    else
                                        MessageBox.Show("Выберите категорию, которую нужно удалить.");
                                    break;
                                }
                        }
                    }      
                }));
            }
        }


        public AdminViewModel(AdminWindow adminWindow)
        {
            this.adminWindow = adminWindow;
            Categories = new ObservableCollection<Category>(Category_Repository.Select());
            OnPropertyChanged("Categories");
            tests = Test_Repository.Select();
            questions = Question_Repository.Select();
            answers = Answer_Repository.Select();
        }

        private void UpdateCurrentAnswers()
        {
            answers = Answer_Repository.Select();

            CurrentAnswer = null;

            CurrentAnswers = null;
            OnPropertyChanged("CurrentAnswers");
        }

        private void UpdateCurrentQuestions()
        {
            questions = Question_Repository.Select();

            CurrentQuestion = null;

            CurrentAnswers = null;
            OnPropertyChanged("CurrentAnswers");
        }

        private void UpdateCurrentTests()
        {
            tests = Test_Repository.Select();

            CurrentTest = null;

            CurrentTests = null;
            OnPropertyChanged("CurrentTests");
        }

        private void UpdateCurrentCategories()
        {
            Categories = new ObservableCollection<Category>(Category_Repository.Select());

            CurrentCategory = null;

            OnPropertyChanged("Categories");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
