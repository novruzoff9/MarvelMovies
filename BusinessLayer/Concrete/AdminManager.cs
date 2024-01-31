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
    public class AdminManager : IAdminService
    {
        IAdminDal _admindal;

        public AdminManager(IAdminDal admindal)
        {
            _admindal = admindal;
        }

        public void Add(Admin p)
        {
            _admindal.Insert(p);
        }

        public void Delete(Admin p)
        {
            throw new NotImplementedException();
        }

        public Admin GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Admin> GetList()
        {
            return _admindal.List();
        }

        public Admin Test(Admin p)
        {
            return _admindal.Check(p);
        }

        public void Update(Admin p)
        {
            throw new NotImplementedException();
        }
    }
}
