using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        List<User> GetList();

        void UserAdd(User p);

        User GetById(int id);

        void UserDelete(User p);

        void UserUpdate(User p);
    }
}
