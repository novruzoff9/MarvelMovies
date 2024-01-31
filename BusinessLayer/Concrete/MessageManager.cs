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
    public class MessageManager : IMessageService
    {
        IRepository<UserMessages> _messagedal;

        public MessageManager(IRepository<UserMessages> messagedal)
        {
            _messagedal = messagedal;
        }

        public void Add(UserMessages p)
        {
            _messagedal.Insert(p);
        }

        public void Delete(UserMessages p)
        {
            throw new NotImplementedException();
        }

        public UserMessages GetByID(int id)
        {
            return _messagedal.Get(x => x.MessageID == id);
        }

        public List<UserMessages> GetListInbox(string mail)
        {
            var messages = _messagedal.List(x => x.ReceiverMail == mail);
            return messages.Where(x => x.RecieverStatus == "active").ToList();
        }

        public List<UserMessages> GetListSelectByReciever(string mail)
        {
            var messages = _messagedal.List(x => x.ReceiverMail == mail);
            return messages.Where(x => x.RecieverStatus == "select").ToList();
        }

        public List<UserMessages> GetListSelectBySender(string mail)
        {
            var messages = _messagedal.List(x => x.SenderMail == mail);
            return messages.Where(x => x.SenderStatus == "select").ToList();
        }

        public List<UserMessages> GetListSendbox(string mail)
        {
            var messages = _messagedal.List(x => x.SenderMail == mail);
            return messages.Where(x => x.SenderStatus == "active").ToList();
        }

        public List<UserMessages> GetListTrashByReciever(string mail)
        {
            var messages = _messagedal.List(x => x.ReceiverMail == mail);
            return messages.Where(x => x.RecieverStatus == "trash").ToList();
        }

        public List<UserMessages> GetListTrashBySender(string mail)
        {
            var messages = _messagedal.List(x => x.SenderMail == mail);
            return messages.Where(x => x.SenderStatus == "trash").ToList();
        }

        public void Update(UserMessages p)
        {
            _messagedal.Update(p);
        }
    }
}
