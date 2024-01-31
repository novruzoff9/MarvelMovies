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
    public class SerieCommentAnswerReactionManager : IMainService<SerieCommentAnswerReaction>
    {
        IRepository<SerieCommentAnswerReaction> _SerieCommentAnswerReactionDal;

        public SerieCommentAnswerReactionManager(IRepository<SerieCommentAnswerReaction> serieCommentAnswerReactionDal)
        {
            _SerieCommentAnswerReactionDal = serieCommentAnswerReactionDal;
        }

        public void Add(SerieCommentAnswerReaction p)
        {
            _SerieCommentAnswerReactionDal.Insert(p);
        }

        public void Delete(SerieCommentAnswerReaction p)
        {
            _SerieCommentAnswerReactionDal.Delete(p);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public SerieCommentAnswerReaction GetByID(int id)
        {
            return _SerieCommentAnswerReactionDal.Get(x=>x.AnswerID == id);
        }

        public List<SerieCommentAnswerReaction> GetList()
        {
            return _SerieCommentAnswerReactionDal.List();
        }

        public void Update(SerieCommentAnswerReaction p)
        {
            _SerieCommentAnswerReactionDal.Update(p);
        }
    }
}
