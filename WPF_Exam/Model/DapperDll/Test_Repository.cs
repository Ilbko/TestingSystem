using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WPF_Exam.DapperDll
{
    public class Test
    {
        public int Test_Id { get; set; }
        public string Test_Name { get; set; }
        public int Test_CategoryId { get; set; }
    }

    public static class Test_Repository
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["TestConnectString"].ConnectionString;

        public static List<Test> Select()
        {
            const string procedure = "EXEC [SelectTest]";

            using (IDbConnection db = new SqlConnection(ConnStr))
            {
                db.Open();
                List<Test> categories;

                try
                {
                    categories = db.Query<Test>(procedure).ToList();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                return categories;
            }
        }

        public static void Delete(Test value)
        {
            const string procedure = "EXEC [DeleteTest] @Test_Id";
            var values = new { Test_Id = value.Test_Id };

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

        public static void Insert(Test value)
        {
            const string procedure = "EXEC [InsertTest] @Test_Name, @Test_CategoryId";
            var values = new { Test_Name = value.Test_Name, Test_CategoryId = value.Test_CategoryId };

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

        public static void Update(Test oldValue, Test newValue)
        {
            const string procedure = "EXEC [UpdateTest] @Test_Id, @Test_Name";
            var values = new { Test_Id = oldValue.Test_Id, Test_Name = newValue.Test_Name };

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
