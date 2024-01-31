using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMovieCommentService
    {
        List<MovieComment> GetList();

        List<MovieComment> GetListByUser(int id);
        List<MovieComment> GetListByMovie(int id);

        void MovieCommentAdd(MovieComment p);

        MovieComment GetByID(int id);

        void MovieCommentDelete(MovieComment p);

        void MovieCommentUpdate(MovieComment p);
    }
}
