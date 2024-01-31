using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFrameWork
{
    public class EfUserDal : GenericRepository<User>, IUserDal
    {
        Context c = new Context();
        public User Check(User p)
        {
            var user = c.Users.FirstOrDefault(x => x.UserName == p.UserName &&
            x.UserPassword == p.UserPassword);
            return user;
        }
    }
}
