using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMainService<T>
    {
        List<T> GetList();

        void Add(T p);

        T GetByID(int id);

        void Delete(T p);
        void DeleteById(int id);

        void Update(T p);
    }
}
