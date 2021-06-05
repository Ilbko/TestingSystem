using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WPF_Exam.DapperDll
{
    public class Category
    {
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
    }

    public static class Category_Repository
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["TestConnectString"].ConnectionString;

        public static List<Category> Select()
        {
            const string procedure = "EXEC [SelectCategory]";

            using (IDbConnection db = new SqlConnection(ConnStr))
            {
                db.Open();
                List<Category> categories;

                try
                {
                    categories = db.Query<Category>(procedure).ToList();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                return categories;
            }
        }

        public static void Delete(Category value)
        {
            const string procedure = "EXEC [DeleteCategory] @Category_Id";
            var values = new { Category_Id = value.Category_Id };

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

        public static void Insert(Category value)
        {
            const string procedure = "EXEC [InsertCategory] @Category_Name";
            var values = new { Category_Name = value.Category_Name };

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

        public static void Update(Category oldValue, Category newValue)
        {
            const string procedure = "EXEC [UpdateCategory] @Category_Id, @Category_Name";
            var values = new { Category_Id = oldValue.Category_Id, Category_Name = newValue.Category_Name };

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
