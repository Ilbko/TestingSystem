using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WPF_Exam.DapperDll
{
    public class Answer
    {
        public int Answer_Id { get; set; }
        public string Answer_Name { get; set; }
        public bool Answer_IsTrue { get; set; }
        public int Answer_QuestionId { get; set; }
    }

    public static class Answer_Repository
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["TestConnectString"].ConnectionString;

        public static List<Answer> Select()
        {
            const string procedure = "EXEC [SelectAnswer]";

            using (IDbConnection db = new SqlConnection(ConnStr))
            {
                db.Open();
                List<Answer> answers;

                try
                {
                    answers = db.Query<Answer>(procedure).ToList();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                return answers;
            }
        }

        public static void Delete(Answer value)
        {
            const string procedure = "EXEC [DeleteAnswer] @Answer_Id";
            var values = new { Answer_Id = value.Answer_Id };

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

        public static void Insert(Answer value)
        {
            const string procedure = "EXEC [InsertAnswer] @Answer_Name, @Answer_IsTrue, @Answer_QuestionId";
            var values = new { Answer_Name = value.Answer_Name, Answer_IsTrue = value.Answer_IsTrue, Answer_QuestionId = value.Answer_QuestionId };

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

        public static void Update(Answer oldValue, Answer newValue)
        {
            const string procedure = "EXEC [UpdateAnswer] @Answer_Id, @Answer_Name, @Answer_IsTrue";
            var values = new { Answer_Id = oldValue.Answer_Id, Answer_Name = newValue.Answer_Name, Answer_IsTrue = newValue.Answer_IsTrue };

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
