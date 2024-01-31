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
    public class MovieCommentManager : IMovieCommentService
    {
        IMovieCommentDal _movieCommentDal;

        public MovieCommentManager(IMovieCommentDal movieCommentDal)
        {
            _movieCommentDal = movieCommentDal;
        }

        public MovieComment GetByID(int id)
        {
            return _movieCommentDal.Get(x => x.MovieCommentID == id);
        }

        public List<MovieComment> GetList()
        {
            return _movieCommentDal.List();
        }

        public List<MovieComment> GetListByMovie(int id)
        {
            return _movieCommentDal.List(x => x.ID == id);
        }

        public List<MovieComment> GetListByUser(int id)
        {
            return _movieCommentDal.List(x => x.UserID == id);
        }

        public void MovieCommentAdd(MovieComment p)
        {
            _movieCommentDal.Insert(p);
        }

        public void MovieCommentDelete(MovieComment p)
        {
            _movieCommentDal.Delete(p);
        }

        public void MovieCommentUpdate(MovieComment p)
        {
            _movieCommentDal.Update(p);
        }
    }
}
