using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MovieManager : IMovieService
    {
        IMovieDal _moviedal;

        public MovieManager(IMovieDal moviedal)
        {
            _moviedal = moviedal;
        }

        public Movie GetByID(int id)
        {
            return _moviedal.Get(x => x.ID == id);
        }

        public List<Movie> GetList()
        {
            return _moviedal.List();
        }

        public void MovieAdd(Movie p)
        {
            _moviedal.Insert(p);
        }

        public void MovieDelete(Movie p)
        {
            _moviedal.Delete(p);            
        }

        public void MovieUpdate(Movie p)
        {
            _moviedal.Update(p);
        }
    }
}
