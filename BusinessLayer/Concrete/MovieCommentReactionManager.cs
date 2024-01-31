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
    public class MovieCommentReactionManager : IMainService<MovieCommentReaction>
    {
        IRepository<MovieCommentReaction> _MovieCommentReactionDal;

        public MovieCommentReactionManager(IRepository<MovieCommentReaction> movieCommentReactionDal)
        {
            _MovieCommentReactionDal = movieCommentReactionDal;
        }

        public void Add(MovieCommentReaction p)
        {
            _MovieCommentReactionDal.Insert(p);
        }

        public void Delete(MovieCommentReaction p)
        {
            _MovieCommentReactionDal.Delete(p);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public MovieCommentReaction GetByID(int id)
        {
            return _MovieCommentReactionDal.Get(x => x.ReactionID == id);
        }

        public List<MovieCommentReaction> GetList()
        {
            return _MovieCommentReactionDal.List();
        }

        public void Update(MovieCommentReaction p)
        {
            _MovieCommentReactionDal.Update(p);
        }
    }
}
