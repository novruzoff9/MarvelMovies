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
    public class ChatManager : IMainService<Chat>
    {
        IRepository<Chat> _Chat;
        public ChatManager(IRepository<Chat> chat)
        {
            _Chat = chat;
        }

        public void Add(Chat p)
        {
            _Chat.Insert(p);
        }

        public void Delete(Chat p)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Chat GetByID(int id)
        {
            return _Chat.Get(x => x.ChatID == id);
        }

        public List<Chat> GetList()
        {
            return _Chat.List();
        }

        public void Update(Chat p)
        {
            _Chat.Update(p);
        }
    }
}
