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
    public class AnimationManager : IMainService<Animation>
    {
        IRepository<Animation> _animationdal;

        public AnimationManager(IRepository<Animation> animationdal)
        {
            _animationdal = animationdal;
        }

        public void Add(Animation p)
        {
            _animationdal.Insert(p);
        }

        public void Delete(Animation p)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Animation GetByID(int id)
        {
            return _animationdal.Get(x => x.ID == id);
        }

        public List<Animation> GetList()
        {
            return _animationdal.List();
        }

        public void Update(Animation p)
        {
            _animationdal.Update(p);
        }
    }
}
