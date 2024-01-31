using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMovieService
    {
        List<Movie> GetList();

        void MovieAdd(Movie p);

        Movie GetByID(int id);

        void MovieDelete(Movie p);

        void MovieUpdate(Movie p);
    }
}
