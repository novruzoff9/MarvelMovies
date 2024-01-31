using BusinessLayer.Concrete;
using BusinessLayer.Validation_Rules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarvelMovies.Controllers
{
    public class UserMessageController : Controller
    {
        MessageManager message = new MessageManager(new EfMessageDal());
        MessageValidator messageval = new MessageValidator();

        // GET: UserMessage
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult MessagesPanel()
        {
            return PartialView();
        }

        public ActionResult Inbox()
        {
            string p = (string)Session["UserMail"];
            ViewBag.mail = p;
            var inboxmessages = message.GetListInbox(p);
            var sendboxmessages = message.GetListSendbox(p);
            List<UserMessages> messages = new List<UserMessages>();
            messages.AddRange(inboxmessages);
            messages.AddRange(sendboxmessages);
            var selectedmessages = message.GetListSelectByReciever(p);
            messages.AddRange(selectedmessages);
            messages = messages.OrderBy(x => x.MessageDate).ToList();
            messages = Enumerable.Reverse(messages).ToList();
            List<UserMessages> dmmessage = new List<UserMessages>();
            foreach (var item in messages)
            {
                if(!dmmessage.Any(x=>x.SenderMail == item.SenderMail))
                {
                    if(!dmmessage.Any(x => x.SenderMail != p))
                    if (!dmmessage.Any(x => x.ReceiverMail == item.ReceiverMail))
                    {
                        dmmessage.Add(item);
                    }
                }                
            }
            return View(dmmessage);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["UserMail"];
            var messages = message.GetListSendbox(p);
            return View(messages);
        }

        public ActionResult Trash()
        {
            string p = (string)Session["UserMail"];
            var recievermessages = message.GetListTrashByReciever(p);
            var sendermessages = message.GetListTrashBySender(p);
            var messages = sendermessages;
            messages.AddRange(recievermessages);
            return View(messages);
        }
        public ActionResult Select()
        {
            string p = (string)Session["UserMail"];
            var recievermessages = message.GetListSelectByReciever(p);
            var sendermessages = message.GetListSelectBySender(p);
            var messages = sendermessages;
            messages.AddRange(recievermessages);
            return View(messages);
        }
        
        public ActionResult SelectMessage(int id)
        {
            string p = (string)Session["UserMail"];
            var selectedmessage = message.GetByID(id);
            if(selectedmessage.ReceiverMail == p)
            {
                selectedmessage.RecieverStatus = "select";                
            }
            else if(selectedmessage.SenderMail == p)
            {
                selectedmessage.SenderStatus = "select";
            }
            message.Update(selectedmessage);
            return RedirectToAction("Inbox");
        }

        public ActionResult TrashMessage(int id)
        {
            string p = (string)Session["UserMail"];
            var selectedmessage = message.GetByID(id);
            if (selectedmessage.ReceiverMail == p)
            {
                selectedmessage.RecieverStatus = "trash";
            }
            else if (selectedmessage.SenderMail == p)
            {
                selectedmessage.SenderStatus = "trash";
            }
            message.Update(selectedmessage);
            return RedirectToAction("Inbox");
        }

        public ActionResult UnReadMessage(int id)
        {
            var unreadmessage = message.GetByID(id);
            unreadmessage.Status = "active";
            message.Update(unreadmessage);
            return RedirectToAction("Inbox");
        }
        public ActionResult ReadMessage(int id)
        {
            var unreadmessage = message.GetByID(id);
            unreadmessage.Status = "passive";
            message.Update(unreadmessage);
            return RedirectToAction("Inbox");
        }
        public ActionResult ActiveMessage(int id)
        {
            string p = (string)Session["UserMail"];
            var selectedmessage = message.GetByID(id);
            if (selectedmessage.ReceiverMail == p)
            {
                selectedmessage.RecieverStatus = "active";
            }
            else if (selectedmessage.SenderMail == p)
            {
                selectedmessage.SenderStatus = "active";
            }
            message.Update(selectedmessage);
            return RedirectToAction("Inbox");
        }
        [HttpGet]
        public ActionResult WriteMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriteMessage(UserMessages p)
        {
            ValidationResult results = messageval.Validate(p);
            p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.Status = "active";
            p.RecieverStatus = "active";
            p.SenderStatus = "active";
            string writer = (string)Session["UserMail"];
            p.SenderMail = writer;
            if (results.IsValid)
            {
                message.Add(p);
                return RedirectToAction("Sendbox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult InboxMessageDetails(int id)
        {
            var messagedet = message.GetByID(id);
            messagedet.Status = "passive";
            message.Update(messagedet);
            return View(messagedet);
        }

        public ActionResult SendboxMessageDetails(int id)
        {
            var messagedet = message.GetByID(id);
            return View(messagedet);
        }

        //public ActionResult ByStatus(string status)
        //{
        //    string p = (string)Session["UserMail"];
        //    if(status == "select")
        //    {
        //        var recievermessages = message.GetListSelectByReciever(p);
        //        var sendermessages = message.GetListSelectBySender(p);
        //        var messages = sendermessages;
        //        messages.AddRange(recievermessages);
        //        return View(messages);
        //    }
        //    else if(status == "trash")
        //    {
        //        var recievermessages = message.GetListTrashByReciever(p);
        //        var sendermessages = message.GetListTrashBySender(p);
        //        var messages = sendermessages;
        //        messages.AddRange(recievermessages);
        //        return View(messages);
        //    }
        //    var messages2 = message.GetListInbox(p);
        //    ViewBag.status = status;
        //    return View(messages2); //Bura bidene xeta sehifesi artirilacag
        //}
    }
}