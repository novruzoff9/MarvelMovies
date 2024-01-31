using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<Admin> GetList();

        void Add(Admin p);

        Admin GetByID(int id);

        Admin Test(Admin p);

        void Delete(Admin p);

        void Update(Admin p);
    }
}
