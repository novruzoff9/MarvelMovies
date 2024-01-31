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
    public class SerieManager : ISerieService
    {
        IRepository<Serie> _seriedal;

        public SerieManager(IRepository<Serie> seriedal)
        {
            _seriedal = seriedal;
        }

        public Serie GetByID(int id)
        {
            return _seriedal.Get(x => x.ID == id);
        }

        public List<Serie> GetList()
        {
            return _seriedal.List();
        }

        public void SerieAdd(Serie p)
        {
            _seriedal.Insert(p);
        }

        public void SerieDelete(Serie p)
        {
            throw new NotImplementedException();
        }

        public void SerieUpdate(Serie p)
        {
            _seriedal.Update(p);
        }
    }
}
