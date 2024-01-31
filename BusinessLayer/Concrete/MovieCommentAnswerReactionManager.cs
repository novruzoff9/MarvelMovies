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
    public class MovieCommentAnswerReactionManager : IMainService<MovieCommentAnswerReaction>
    {
        IRepository<MovieCommentAnswerReaction> _MovieCommentAnswerReactionDal;

        public MovieCommentAnswerReactionManager(IRepository<MovieCommentAnswerReaction> movieCommentAnswerReactionDal)
        {
            _MovieCommentAnswerReactionDal = movieCommentAnswerReactionDal;
        }

        public void Add(MovieCommentAnswerReaction p)
        {
            _MovieCommentAnswerReactionDal.Insert(p);
        }

        public void Delete(MovieCommentAnswerReaction p)
        {
            _MovieCommentAnswerReactionDal.Delete(p);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public MovieCommentAnswerReaction GetByID(int id)
        {
            return _MovieCommentAnswerReactionDal.Get(x => x.ReactionID == id);
        }

        public List<MovieCommentAnswerReaction> GetList()
        {
            return _MovieCommentAnswerReactionDal.List();
        }

        public void Update(MovieCommentAnswerReaction p)
        {
            _MovieCommentAnswerReactionDal.Update(p);
        }
    }
}
