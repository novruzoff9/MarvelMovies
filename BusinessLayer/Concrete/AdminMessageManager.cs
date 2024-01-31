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
    public class AdminMessageManager : IMainService<AdminMessages>
    {
        IRepository<AdminMessages> _AdminMessagedal;

        public AdminMessageManager(IRepository<AdminMessages> adminMessagedal)
        {
            _AdminMessagedal = adminMessagedal;
        }

        public void Add(AdminMessages p)
        {
            _AdminMessagedal.Insert(p);
        }

        public void Delete(AdminMessages p)
        {
            _AdminMessagedal.Delete(p);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public AdminMessages GetByID(int id)
        {
            return _AdminMessagedal.Get(x => x.MessageID == id);
        }

        public List<AdminMessages> GetList()
        {
            return _AdminMessagedal.List();
        }

        public void Update(AdminMessages p)
        {
            _AdminMessagedal.Update(p);
        }
    }
}
