using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MarvelMovies.Controllers
{
    public class DirectMessageController : Controller
    {
        ChatManager ChatM = new ChatManager(new EfChatDal());
        ChatMessageManager ChatMessage = new ChatMessageManager(new EfChatMessageDal());
        // GET: DirectMessage
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Chats()
        {
            int userid = (int)Session["UserID"];
            ViewBag.id = userid;
            var chats = ChatM.GetList();
            var user1stchats = chats.Where(x => x.User1stID == userid).ToList();
            var user2ndchats = chats.Where(x => x.User2ndID == userid).ToList();
            List<Chat> userchats = new List<Chat>();
            userchats.AddRange(user1stchats);
            userchats.AddRange(user2ndchats);
            userchats = userchats.OrderBy(x => x.LastMessage).Reverse().ToList();
            return PartialView(userchats);
        }

        public ActionResult Direct(int id)
        {
            int userid = (int)Session["UserID"];
            ViewBag.id = userid;
            ViewBag.chatid = id;
            var messages = ChatMessage.GetList();
            int newmessage = -1;
            messages = messages.Where(x => x.ChatID == id).ToList();
            int n = messages.Count();
            ViewBag.messagecount = n;
            var lastmessage = messages[n-1];
            n -= 1;
            if (lastmessage.WriterID != userid)
            {
                while(lastmessage.Seen == false)
                {
                    newmessage = lastmessage.MessageID;
                    lastmessage.Seen = true;
                    ChatMessage.Update(lastmessage);
                    lastmessage = messages[n - 1];
                    n -= 1;
                }
            }
            ViewBag.newmessages = newmessage;
            ViewBag.lastmessageid = lastmessage.MessageID;
            return View(messages);
        }



        public JsonResult MessagesCount(int id)
        {
            var messages = ChatMessage.GetList();
            messages = messages.Where(x => x.ChatID == id).ToList();
            int n = messages.Count();
            return Json(n, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Messages(int id)
        {
            var messages = ChatMessage.GetList();
            messages = messages.Where(x => x.ChatID == id).ToList();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmptyDirect()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult WriteMessage(int id)
        {
            ViewBag.chatid = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult WriteMessage(ChatMessage p)
        {
            p.WriterID = (int)Session["UserID"];
            p.MessageDate = DateTime.Parse(DateTime.Now.ToLongTimeString());
            var chatid = p.ChatID;
            ChatMessage.Add(p);
            var chat = ChatM.GetByID(chatid);
            chat.LastMessage = p.MessageDate;
            ChatM.Update(chat);
            return Content("");
            //return RedirectToAction("Direct", new { id = p.ChatID } );
        }    
        
        [HttpPost]
        public JsonResult AddMessage(ChatMessage p)
        {
            p.WriterID = (int)Session["UserID"];
            p.MessageDate = DateTime.Parse(DateTime.Now.ToLongTimeString());
            var chatid = p.ChatID;
            ChatMessage.Add(p);
            var chat = ChatM.GetByID(chatid);
            chat.LastMessage = p.MessageDate;
            ChatM.Update(chat);
            return Json(p,JsonRequestBehavior.AllowGet);
        }


        public ActionResult NewChat(int id)
        {
            ViewBag.userid = id;
            int userid = (int)Session["UserID"];
            var chats1 = ChatM.GetList();
            var chats2 = ChatM.GetList();
            chats1 = chats1.FindAll(x => x.User1stID == id);
            chats1 = chats1.FindAll(x => x.User2ndID == userid);
            chats2 = chats2.FindAll(x => x.User2ndID == id);
            chats2 = chats2.FindAll(x => x.User1stID == userid);
            if (chats1.Count() > 0)
            {
                var chatid = chats1.First().ChatID;
                return RedirectToAction("Direct", new { id = chatid });
            }
            else if (chats2.Count() > 0)
            {
                var chatid = chats2.First().ChatID;
                return RedirectToAction("Direct", new { id = chatid });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public PartialViewResult WriteNewChat(int id)
        {
            ViewBag.User2ndID = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult WriteNewChat(ChatMessage p)
        {
            int User2nd = Request.Form["User2ndID"].AsInt();
            Chat NewChat = new Chat();
            NewChat.User1stID = (int)Session["UserID"];
            NewChat.User2ndID = User2nd;
            NewChat.LastMessage = DateTime.Parse(DateTime.Now.ToLongTimeString());
            ChatM.Add(NewChat);
            p.ChatID = NewChat.ChatID;
            p.WriterID = (int)Session["UserID"];
            p.MessageDate = DateTime.Parse(DateTime.Now.ToLongTimeString());
            ChatMessage.Add(p);
            return RedirectToAction("Direct", new { id = p.ChatID });
        }
    }
}