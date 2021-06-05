using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WPF_Exam.DapperDll;
using WPF_Exam.View.AdminWindowFolder;

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

        //internal static void Add(int workLevel, int category_Id, int test_Id, int question_Id)
        //{
        //    switch (workLevel)
        //    {
        //        case 3:
        //            {
        //                new AddUpdateWindow(workLevel, question_Id).ShowDialog();
        //                break;                         
        //            }                                  
        //        case 2:                                
        //            {                                  
        //                new AddUpdateWindow(workLevel, test_Id).ShowDialog();
        //                break;                         
        //            }                                  
        //        case 1:                                
        //            {                                  
        //                new AddUpdateWindow(workLevel, category_Id).ShowDialog();
        //                break;
        //            }
        //        case 0:
        //            {
        //                new AddUpdateWindow(workLevel, 0).ShowDialog();
        //                break;
        //            }
        //    }
        //}

        internal static void Add(int workLevel, int foreign_Id) => new AddUpdateWindow(workLevel, foreign_Id).ShowDialog();

        internal static void AddEntry(int workLevel, int foreign_Id, string name, bool isTrue)
        {
            switch (workLevel)
            {
                case 3:
                    {
                        Answer_Repository.Insert(new Answer { Answer_Name = name, Answer_IsTrue = isTrue, Answer_QuestionId = foreign_Id });
                        break;
                    }
                case 2:
                    {
                        Question_Repository.Insert(new Question { Question_Name = name, Question_TestId = foreign_Id });
                        break;
                    }
                case 1:
                    {
                        Test_Repository.Insert(new Test { Test_Name = name, Test_CategoryId = foreign_Id });
                        break;
                    }
                case 0:
                    {
                        Category_Repository.Insert(new Category { Category_Name = name });
                        break;
                    }
            }
        }

        internal static void Update(int workLevel, object currentItem) => new AddUpdateWindow(workLevel, currentItem).ShowDialog();

        internal static void UpdateEntry(int workLevel, object currentItem, string name, bool isTrue)
        {
            switch (workLevel)
            {
                case 3:
                    {
                        Answer_Repository.Update(currentItem as Answer, new Answer { Answer_Name = name, Answer_IsTrue = isTrue});
                        break;
                    }
                case 2:
                    {
                        Question_Repository.Update(currentItem as Question, new Question { Question_Name = name });
                        break;
                    }
                case 1:
                    {
                        Test_Repository.Update(currentItem as Test, new Test { Test_Name = name });
                        break;
                    }
                case 0:
                    {
                        Category_Repository.Update(currentItem as Category, new Category { Category_Name = name });
                        break;
                    }
            }
        }
    }
}
