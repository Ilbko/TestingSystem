using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WPF_Exam.DapperDll
{
    public class User
    {
        public int User_Id { get; set; }
        public string User_Login { get; set; }
        public string User_Password { get; set; }
    }


    public static class User_Repository
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["TestConnectString"].ConnectionString;

        public static List<User> Select()
        {
            const string procedure = "EXEC [SelectUser]";

            using (IDbConnection db = new SqlConnection(ConnStr))
            {
                db.Open();
                List<User> users;

                try
                {
                    users = db.Query<User>(procedure).ToList();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }               

                return users;
            }
        }

        public static void Delete(User value)
        {
            const string procedure = "EXEC [DeleteUser] @User_Id";
            var values = new { User_Id = value.User_Id };

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

        public static void Insert(User value)
        {
            const string procedure = "EXEC [InsertUser] @User_Login, @User_Password";
            var values = new { User_Login = value.User_Login, User_Password = value.User_Password };

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

        public static void Update(User oldValue, User newValue)
        {
            const string procedure = "EXEC [UpdateUser] @User_Id, @User_Login, @User_Password";
            var values = new { User_Id = oldValue.User_Id, User_Login = newValue.User_Login, User_Password = newValue.User_Password };

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
