using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class User : IUser
    {
        InsurewaveContext db;
        public User()
        {
            db = new InsurewaveContext();
        }
        public bool LoginUser(UserDetail userdetail)
        {
            object temp = db.UserDetails.Where(u => (u.MailId== userdetail.MailId && u.Password == userdetail.Password)).FirstOrDefault();
            if (temp != null)
                return true;
            else
                return false;

        }
        public void AddUser(UserDetail userdetail)
        {
            db.UserDetails.Add(userdetail);
            db.SaveChanges();
        }
    }
}
