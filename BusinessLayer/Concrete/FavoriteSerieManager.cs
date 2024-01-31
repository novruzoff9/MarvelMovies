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
    public class FavoriteSerieManager : IMainService<FavoriteSerie>
    {
        IRepository<FavoriteSerie> _FavoriteSeries;

        public FavoriteSerieManager(IRepository<FavoriteSerie> favoriteSeries)
        {
            _FavoriteSeries = favoriteSeries;
        }

        public void Add(FavoriteSerie p)
        {
            _FavoriteSeries.Insert(p);
        }

        public void Delete(FavoriteSerie p)
        {
            _FavoriteSeries.Delete(p);
        }

        public void DeleteById(int id)
        {
            FavoriteSerie p = _FavoriteSeries.Get(x => x.ID == id);
            _FavoriteSeries.Delete(p);
        }

        public FavoriteSerie GetByID(int id)
        {
            return _FavoriteSeries.Get(x => x.ID == id);
        }

        public List<FavoriteSerie> GetList()
        {
            return _FavoriteSeries.List();
        }

        public void Update(FavoriteSerie p)
        {
            _FavoriteSeries.Update(p);
        }
    }
}
