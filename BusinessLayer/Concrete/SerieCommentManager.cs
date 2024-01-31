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
    public class SerieCommentManager : ICommentService<SerieComment>
    {
        IRepository<SerieComment> _seriecomdal;

        public SerieCommentManager(IRepository<SerieComment> seriecomdal)
        {
            _seriecomdal = seriecomdal;
        }

        public void Add(SerieComment p)
        {
            _seriecomdal.Insert(p);
        }

        public void Delete(SerieComment p)
        {
            throw new NotImplementedException();
        }

        public SerieComment GetByID(int id)
        {
            return _seriecomdal.Get(x => x.SerieCommentID == id);
        }

        public List<SerieComment> GetList()
        {
            return _seriecomdal.List();
        }

        public List<SerieComment> GetListByMovie(int id)
        {
            return _seriecomdal.List(x => x.ID == id);
        }

        public List<SerieComment> GetListByUser(int id)
        {
            return _seriecomdal.List(x => x.UserID == id);
        }

        public void MovieCommentAdd(SerieComment p)
        {
            _seriecomdal.Insert(p);
        }

        public void MovieCommentDelete(SerieComment p)
        {
            throw new NotImplementedException();
        }

        public void MovieCommentUpdate(SerieComment p)
        {
            throw new NotImplementedException();
        }

        public void Update(SerieComment p)
        {
            _seriecomdal.Update(p);
        }
    }
}
