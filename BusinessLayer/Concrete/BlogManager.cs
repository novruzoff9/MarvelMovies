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
    public class BlogManager : IMainService<Blog>
    {
        IRepository<Blog> _BlogDal;

        public BlogManager(IRepository<Blog> blogDal)
        {
            _BlogDal = blogDal;
        }

        public void Add(Blog p)
        {
            _BlogDal.Insert(p);
        }

        public void Delete(Blog p)
        {
            _BlogDal.Delete(p);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Blog GetByID(int id)
        {
            return _BlogDal.Get(x => x.BlogID == id);
        }

        public List<Blog> GetList()
        {
            return _BlogDal.List();
        }

        public void Update(Blog p)
        {
            _BlogDal.Update(p);
        }
    }
}
