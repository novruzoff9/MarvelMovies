using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userdal;

        public UserManager(IUserDal userdal)
        {
            _userdal = userdal;
        }

        public User GetById(int id)
        {
            return _userdal.Get(x => x.UserID == id);
        }

        public List<User> GetList()
        {
            return _userdal.List();
        }

        public void UserAdd(User p)
        {
            _userdal.Insert(p);
        }

        public void UserDelete(User p)
        {
            _userdal.Delete(p);
        }

        public User Test(User p)
        {
            return _userdal.Check(p);
        }

        public void UserUpdate(User p)
        {
            _userdal.Update(p);
        }
    }
}
