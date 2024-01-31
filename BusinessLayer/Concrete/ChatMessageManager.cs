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
    public class ChatMessageManager : IMainService<ChatMessage>
    {
        IRepository<ChatMessage> _ChatMessage;

        public ChatMessageManager(IRepository<ChatMessage> chatMessage)
        {
            _ChatMessage = chatMessage;
        }

        public void Add(ChatMessage p)
        {
            _ChatMessage.Insert(p);
        }

        public void Delete(ChatMessage p)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public ChatMessage GetByID(int id)
        {
            return _ChatMessage.Get(x => x.MessageID == id);
        }

        public List<ChatMessage> GetList()
        {
            return _ChatMessage.List();
        }

        public void Update(ChatMessage p)
        {
            _ChatMessage.Update(p);
        }
    }
}
