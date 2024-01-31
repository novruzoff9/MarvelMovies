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
    public class MovieCommentAnswerManager : IMainService<MovieCommentAnswer>
    {

        IRepository<MovieCommentAnswer> _MovieCommentAnswerDal;

        public MovieCommentAnswerManager(IRepository<MovieCommentAnswer> movieCommentAnswerDal)
        {
            _MovieCommentAnswerDal = movieCommentAnswerDal;
        }

        public void Add(MovieCommentAnswer p)
        {
            _MovieCommentAnswerDal.Insert(p);
        }

        public void Delete(MovieCommentAnswer p)
        {
            _MovieCommentAnswerDal.Delete(p);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public MovieCommentAnswer GetByID(int id)
        {
            return _MovieCommentAnswerDal.Get(x => x.AnswerID == id);
        }

        public List<MovieCommentAnswer> GetList()
        {
            return _MovieCommentAnswerDal.List();
        }

        public void Update(MovieCommentAnswer p)
        {
            _MovieCommentAnswerDal.Update(p);
        }
    }
}
