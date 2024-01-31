using BusinessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarvelMovies.Controllers
{
    public class BlogController : Controller
    {
        BlogManager BlogM = new BlogManager(new EfBlogDal());

        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Blogs()
        {
            var blogs = BlogM.GetList();
            return View(blogs);
        }

        public ActionResult BlogDetails(int id)
        {
            var blog = BlogM.GetByID(id);
            return View(blog);
        }

        public PartialViewResult RecommendedBlogs()
        {
            var blogs = BlogM.GetList();
            return PartialView(blogs);
        }

        [HttpGet]
        public ActionResult WriteBlog()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriteBlog(Blog p)
        {
            int userid = (int)Session["UserID"];
            p.UserID = userid;
            var blogs = BlogM.GetList();
            int newid;
            try
            {
                newid = blogs.Last().BlogID + 1;
            }
            catch (Exception)
            {
                newid = 1;
            }
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string address = "~/Images/BlogImages/" + newid + filename;
                Request.Files[0].SaveAs(Server.MapPath(address));
                p.BlogImage = "/Images/BlogImages/" + newid + filename;
            }
            p.BlogDate = DateTime.Parse(DateTime.Now.ToLongTimeString());
            BlogM.Add(p);
            return RedirectToAction("Blogs");
        }
    }
}