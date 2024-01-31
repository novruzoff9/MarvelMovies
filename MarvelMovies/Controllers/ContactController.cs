using BusinessLayer.Concrete;
using BusinessLayer.Validation_Rules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarvelMovies.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        AdminMessageManager adminMessage = new AdminMessageManager(new EfAdminMessageDal());
        AdminMessageValidator AdMesValidator = new AdminMessageValidator();
        Context C = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult MessagesPanel()
        {
            return PartialView();
        }

        public ActionResult MessagesList()
        {
            var messages = adminMessage.GetList();            
            return View(messages);
        }

        public ActionResult MessageDetails(int id)
        {
            var message = adminMessage.GetByID(id);
            return View(message);
        }

        public ActionResult DeletedMessages()
        {
            return View();
        }
    }
}