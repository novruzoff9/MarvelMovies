using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<UserMessages> GetListInbox(string mail);
        List<UserMessages> GetListSendbox(string mail);
        List<UserMessages> GetListTrashByReciever(string mail);
        List<UserMessages> GetListTrashBySender(string mail);
        List<UserMessages> GetListSelectByReciever(string mail);
        List<UserMessages> GetListSelectBySender(string mail);

        void Add(UserMessages p);

        UserMessages GetByID(int id);

        void Delete(UserMessages p);

        void Update(UserMessages p);
    }
}
