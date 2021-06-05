using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Exam.DapperDll
{
    public class Question
    {
        public int Question_Id { get; set; }
        public string Question_Name { get; set; }
        public int Question_TestId { get; set; }
    }

    public static class Question_Repository
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["TestConnectString"].ConnectionString;

        public static List<Question> Select()
        {
            const string procedure = "EXEC [SelectQuestion]";

            using (IDbConnection db = new SqlConnection(ConnStr))
            {
                db.Open();
                List<Question> questions;

                try
                {
                    questions = db.Query<Question>(procedure).ToList();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                
                return questions;
            }
        }

        public static void Delete(Question value)
        {
            const string procedure = "EXEC [DeleteQuestion] @Question_Id";
            var values = new { Question_Id = value.Question_Id };

            using (IDbConnection db = new SqlConnection(ConnStr))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Query(procedure, values, transaction);
                        transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static void Insert(Question value)
        {
            const string procedure = "EXEC [InsertQuestion] @Question_Name, @Quesiton_TestId";
            var values = new { Question_Name = value.Question_Name, Question_TestId = value.Question_TestId };

            using (IDbConnection db = new SqlConnection(ConnStr))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Query(procedure, values, transaction);
                        transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static void Update(Question oldValue, Question newValue)
        {
            const string procedure = "EXEC [UpdateQuestion] @Question_Id, @Question_Name";
            var values = new { Question_Id = oldValue.Question_Id, Question_Name = newValue.Question_Name };

            using (IDbConnection db = new SqlConnection(ConnStr))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Query(procedure, values, transaction);
                        transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
