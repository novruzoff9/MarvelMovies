using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService<T>
    {
        List<T> GetList();

        List<T> GetListByUser(int id);
        List<T> GetListByMovie(int id);

        void MovieCommentAdd(T p);

        T GetByID(int id);

        void MovieCommentDelete(T p);

        void MovieCommentUpdate(T p);
    }
}
