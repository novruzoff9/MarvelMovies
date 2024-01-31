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
    public class SerieCommentAnswerManager : IMainService<SerieCommentAnswer>
    {
        IRepository<SerieCommentAnswer> _SerieCommentAnswerDal;

        public SerieCommentAnswerManager(IRepository<SerieCommentAnswer> serieCommentAnswerDal)
        {
            _SerieCommentAnswerDal = serieCommentAnswerDal;
        }

        public void Add(SerieCommentAnswer p)
        {
            _SerieCommentAnswerDal.Insert(p);
        }

        public void Delete(SerieCommentAnswer p)
        {
            _SerieCommentAnswerDal.Delete(p);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public SerieCommentAnswer GetByID(int id)
        {
            return _SerieCommentAnswerDal.Get(x => x.AnswerID == id);
        }

        public List<SerieCommentAnswer> GetList()
        {
            return _SerieCommentAnswerDal.List();
        }

        public void Update(SerieCommentAnswer p)
        {
            _SerieCommentAnswerDal.Update(p);
        }
    }
}
