using System.Collections.Generic;
using System.Linq;
using WPF_Exam.DapperDll;
using WPF_Exam.View.Admin;

namespace WPF_Exam.Model
{
    public static class Logic
    {
        internal static bool Auth(string loginStr, string passStr, bool isAdmin)
        {
            if (isAdmin)
            {
                List<Admin> admins = Admin_Repository.Select();              
                Admin admin;

                if ((admin = admins.SingleOrDefault(x => x.Admin_Login == loginStr && x.Admin_Password == passStr)) == null)
                    return false;

                new AdminWindow().Show();
                return true;               
            } 
            else
            {
                List<User> users = User_Repository.Select();
                User user;

                if ((user = users.SingleOrDefault(x => x.User_Login == loginStr && x.User_Password == passStr)) == null)
                    return false;

                new UserWindow().Show();
                return true;
            }
        }
    }
}
