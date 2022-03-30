using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IUser
    {
        public bool LoginUser(UserDetail userdetail);
        public void AddUser(UserDetail userdetail);
    }
}
