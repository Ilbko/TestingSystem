using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WPF_Exam.DapperDll;
using WPF_Exam.View.AdminWindowFolder;
using WPF_Exam.View.UserWindowFolder;

namespace WPF_Exam.Model
{
    public static class Logic
    {
        internal static bool Auth(string loginStr, string passStr, bool isAdmin)
        {
            if (isAdmin)
            {
                List<Admin> admins = Admin_Repository.Select();              
                Admin currentAdmin;

                if ((currentAdmin = admins.SingleOrDefault(x => x.Admin_Login == loginStr && x.Admin_Password == passStr)) == null)
                    return false;

                new AdminWindow(currentAdmin).Show();
                //AdminWindow window = new AdminWindow();
                //window.Show();
                return true;               
            } 
            else
            {
                List<User> users = User_Repository.Select();
                User currentUser;

                if ((currentUser = users.SingleOrDefault(x => x.User_Login == loginStr && x.User_Password == passStr)) == null)
                    return false;

                //new UserWindow(currentUser).Show();
                return true;
            }
        }

        internal static void Delete(Answer currentAnswer,  List<Answer> answers)
        {
            Answer_Repository.Delete(currentAnswer);
        }

        internal static void Delete(Question currentQuestion,  List<Answer> answers,  List<Question> questions)
        {
            foreach(Answer answerItem in answers)
            {
                if (answerItem.Answer_QuestionId == currentQuestion.Question_Id)
                    Answer_Repository.Delete(answerItem);
            }

            Question_Repository.Delete(currentQuestion);
        }

        internal static void Delete(Test currentTest,  List<Answer> answers,  List<Question> questions,  List<Test> tests)
        {
            foreach(Question questionItem in questions)
            {
                if (questionItem.Question_TestId == currentTest.Test_Id)
                {
                    foreach(Answer answerItem in answers)
                    {
                        if (answerItem.Answer_QuestionId == questionItem.Question_Id)
                            Answer_Repository.Delete(answerItem);
                    }
                    Question_Repository.Delete(questionItem);
                }
            }

            Test_Repository.Delete(currentTest);
        }

        internal static void Delete(Category currentCategory,  List<Answer> answers,  List<Question> questions,  List<Test> tests,  ObservableCollection<Category> Categories)
        {
            foreach(Test testItem in tests)
            {
                if (testItem.Test_CategoryId == currentCategory.Category_Id)
                {
                    foreach(Question questionItem in questions)
                    {
                        if (questionItem.Question_TestId == testItem.Test_Id)
                        {
                            foreach(Answer answerItem in answers)
                            {
                                if (answerItem.Answer_QuestionId == questionItem.Question_Id)
                                    Answer_Repository.Delete(answerItem);
                            }
                            Question_Repository.Delete(questionItem);
                        }
                    }
                    Test_Repository.Delete(testItem);
                }
            }

            Category_Repository.Delete(currentCategory);
        }
    }
}
