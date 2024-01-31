using BusinessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarvelMovies.Controllers
{
    public class SerieCommentController : Controller
    {
        // GET: SerieComment
        SerieCommentManager seriecomment = new SerieCommentManager(new EfSerieCommentDal());
        SerieManager serie = new SerieManager(new EfSerieDal());
        UserManager user = new UserManager(new EfUserDal());
        SerieCommentAnswerManager sericomans = new SerieCommentAnswerManager(new EfSerieCommentAnswerDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SerieCommentsList()
        {
            var comments = seriecomment.GetList();
            return View(comments);
        }

        [HttpGet]
        public ActionResult AddSerieComment()
        {
            List<SelectListItem> valueserie = (from x in serie.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.ID.ToString()
                                               }).ToList();
            ViewBag.vls = valueserie;
            List<SelectListItem> valueuser = (from x in user.GetList()
                                              select new SelectListItem
                                              {
                                                  Text = x.UserName,
                                                  Value = x.UserID.ToString()
                                              }).ToList();
            ViewBag.vlu = valueuser;
            return View();
        }
        [HttpPost]
        public ActionResult AddSerieComment(SerieComment p)
        {
            p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            seriecomment.Add(p);
            return RedirectToAction("SerieCommentsList");
        }

        public ActionResult DelSerieComment(int id)
        {
            var comment = seriecomment.GetByID(id);
            comment.CommentStatus = false;
            seriecomment.Update(comment);
            return RedirectToAction("SerieCommentsList");
        }

        public ActionResult ActiveSerieComment(int id)
        {
            var comment = seriecomment.GetByID(id);
            comment.CommentStatus = true;
            seriecomment.Update(comment);
            return RedirectToAction("SerieCommentsList");
        }

        public ActionResult CommentBySerie(int id)
        {
            ViewBag.id = id;
            ViewBag.name = serie.GetByID(id).Name;
            var comments = seriecomment.GetList();
            return View(comments);
        }

        public ActionResult CommentByUser(int id)
        {
            //Gorunum qalib
            ViewBag.id = id;
            ViewBag.username = user.GetById(id).UserName;
            var comments = seriecomment.GetList();
            return View(comments);
        }
        public PartialViewResult SerieCommentsByUser(int id)
        {
            var comments = seriecomment.GetList();
            comments = comments.Where(x => x.UserID == id).ToList();
            return PartialView(comments);
        }

        public PartialViewResult SerieCommentAnswersByUser(int id)
        {
            var answers = sericomans.GetList();
            answers = answers.Where(x => x.UserId == id).ToList();
            return PartialView(answers);
        }
    }
}