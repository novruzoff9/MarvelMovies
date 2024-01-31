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
    public class SerieCommentReactionManager : IMainService<SerieCommentReaction>
    {
        IRepository<SerieCommentReaction> _SerieCommentReactionDal;

        public SerieCommentReactionManager(IRepository<SerieCommentReaction> serieCommentReactionDal)
        {
            _SerieCommentReactionDal = serieCommentReactionDal;
        }

        public void Add(SerieCommentReaction p)
        {
            _SerieCommentReactionDal.Insert(p);
        }

        public void Delete(SerieCommentReaction p)
        {
            _SerieCommentReactionDal.Delete(p);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public SerieCommentReaction GetByID(int id)
        {
            return _SerieCommentReactionDal.Get(x => x.ReactionID == id);
        }

        public List<SerieCommentReaction> GetList()
        {
            return _SerieCommentReactionDal.List();
        }

        public void Update(SerieCommentReaction p)
        {
            _SerieCommentReactionDal.Update(p);
        }
    }
}
