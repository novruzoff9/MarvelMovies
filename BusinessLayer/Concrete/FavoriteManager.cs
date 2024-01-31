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
    public class FavoriteManager : IMainService<Favorites>
    {
        IRepository<Favorites> _favoritesdal;

        public FavoriteManager(IRepository<Favorites> favoritesdal)
        {
            _favoritesdal = favoritesdal;
        }

        public void Add(Favorites p)
        {
            _favoritesdal.Insert(p);
        }

        public void Delete(Favorites p)
        {
            _favoritesdal.Delete(p);
        }

        public void DeleteById(int id)
        {
            Favorites p = _favoritesdal.Get(x => x.ID == id);
            _favoritesdal.Delete(p);
        }

        public Favorites GetByID(int id)
        {
            return _favoritesdal.Get(x => x.ID == id);
        }

        public List<Favorites> GetList()
        {
            return _favoritesdal.List();
        }

        public void Update(Favorites p)
        {
            throw new NotImplementedException();
        }
    }
}
