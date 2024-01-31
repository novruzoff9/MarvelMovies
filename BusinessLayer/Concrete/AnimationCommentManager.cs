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
    public class AnimationCommentManager : ICommentService<AnimationComment>
    {
        IRepository<AnimationComment> _animationcomdal;

        public AnimationCommentManager(IRepository<AnimationComment> animationcomdal)
        {
            _animationcomdal = animationcomdal;
        }

        public AnimationComment GetByID(int id)
        {
            return _animationcomdal.Get(x => x.MovieCommentID == id);
        }

        public List<AnimationComment> GetList()
        {
            return _animationcomdal.List();
        }

        public List<AnimationComment> GetListByMovie(int id)
        {
            return _animationcomdal.List(x => x.ID == id);
        }

        public List<AnimationComment> GetListByUser(int id)
        {
            return _animationcomdal.List(x => x.UserID == id);
        }

        public void MovieCommentAdd(AnimationComment p)
        {
            throw new NotImplementedException();
        }

        public void MovieCommentDelete(AnimationComment p)
        {
            throw new NotImplementedException();
        }

        public void MovieCommentUpdate(AnimationComment p)
        {
            throw new NotImplementedException();
        }
    }
}
