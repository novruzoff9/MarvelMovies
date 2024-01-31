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
    public class MessageController : Controller
    {
        MessageManager message = new MessageManager(new EfMessageDal());
        MessageValidator messageval = new MessageValidator();
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            string p = (string)Session["AdminMail"];
            ViewBag.mail = p;
            var messages = message.GetListInbox(p);
            return View(messages);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["AdminMail"];
            var messages = message.GetListSendbox(p);
            return View(messages);
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
            return View(messagedet);
        }

        public ActionResult SendboxMessageDetails(int id)
        {
            var messagedet = message.GetByID(id);
            return View(messagedet);
        }
    }
}