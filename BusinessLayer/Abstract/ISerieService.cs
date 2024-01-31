using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ISerieService
    {
        List<Serie> GetList();

        void SerieAdd(Serie p);

        Serie GetByID(int id);

        void SerieDelete(Serie p);

        void SerieUpdate(Serie p);
    }
}
