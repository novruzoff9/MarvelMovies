using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MarvelMovies.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        AdminManager am = new AdminManager(new EfAdminDal());
        UserManager um = new UserManager(new EfUserDal());
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var admininfo = am.Test(p);
            if (admininfo != null)
            {
                FormsAuthentication.SetAuthCookie(admininfo.AdminName, false);
                Session["AdminName"] = admininfo.AdminName;
                Session["AdminMail"] = admininfo.AdminMail;
                return RedirectToAction("MoviesList", "Movie");
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost]
        public ActionResult UserLogin(User p)
        {
            var userinfo = um.Test(p);
            if (userinfo != null)
            {
                FormsAuthentication.SetAuthCookie(userinfo.UserName, false);
                Session["UserName"] = userinfo.UserName;
                Session["UserMail"] = userinfo.UserMail;
                Session["UserID"] = userinfo.UserID;
                return RedirectToAction("MyProfile", "Profile");
            }
            else
            {
                return RedirectToAction("XMenUniverse", "Universe");
            }
        }
        [HttpPost]
        public ActionResult UserSignUp(User p)
        {
            p.UserSignDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            var userinfo = c.Users.FirstOrDefault(x => x.UserName == p.UserName ||
            x.UserMail == p.UserMail);
            if (userinfo == null)
            {
                p.UserImage = "/Images/UserProfilePhotos/DefaultUserImage.jpg";
                um.UserAdd(p);
                userinfo = um.GetById(p.UserID);
                FormsAuthentication.SetAuthCookie(userinfo.UserName, false);
                Session["UserName"] = userinfo.UserName;
                Session["UserMail"] = userinfo.UserMail;
                Session["UserID"] = userinfo.UserID;
                return RedirectToAction("MyProfile", "Profile");
            }
            else
            {
                return RedirectToAction("XMenUniverse", "Universe");
            }
        }

        public ActionResult UserLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("MarvelUniverse", "Universe");
        }
    }
}