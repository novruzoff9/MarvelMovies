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
    public class EfAdminDal : GenericRepository<Admin>, IAdminDal
    {
        Context c = new Context();
        public Admin Check(Admin p)
        {
            var admin = c.Admins.FirstOrDefault(x => x.AdminName == p.AdminName &&
            x.AdminPassword == p.AdminPassword);
            return admin;
        }
    }
}
