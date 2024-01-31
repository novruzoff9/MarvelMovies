using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarvelMovies.Controllers
{
    public class SerieDetailsController : Controller
    {
        SerieManager serie = new SerieManager(new EfSerieDal());
        FavoriteSerieManager favorite = new FavoriteSerieManager(new EfFavoriteSerieDal());
        SerieCommentManager seriecomment = new SerieCommentManager(new EfSerieCommentDal());
        SerieCommentReactionManager sericomreact = new SerieCommentReactionManager(new EfSerieCommentReactionDal());
        SerieCommentAnswerManager sericomans = new SerieCommentAnswerManager(new EfSerieCommentAnswerDal());
        SerieCommentAnswerReactionManager sericomansreact = new SerieCommentAnswerReactionManager(new EfSerieCommentAnswerReactionDal());
        Context c = new Context();
        // GET: SerieDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Serie(int id)
        {
            var serieinfo = serie.GetByID(id);
            var comments = seriecomment.GetList();
            var answers = sericomans.GetList();
            ViewBag.serieid = id;
            int commentcount = seriecomment.GetListByMovie(id).Count();
            commentcount += answers.Where(x => x.SerieComment.ID == id).Count();
            ViewBag.commentcount = commentcount;
            return View(serieinfo);
        }

        //public ActionResult AddFavorite(int id, string username)
        //{
        //    FavoriteSerie p = new FavoriteSerie();
        //    p.SerieID = id;
        //    username = (string)Session["UserName"];
        //    var userid = c.Users.Where(x => x.UserName == username).
        //        Select(y => y.UserID).FirstOrDefault();
        //    p.UserID = userid;
        //    favorite.Add(p);
        //    return RedirectToAction("MarvelUniverse", "Universe");
        //}

        [HttpPost]
        public JsonResult AddFavorite(int id, string username)
        {
            FavoriteSerie p = new FavoriteSerie();
            p.SerieID = id;
            username = (string)Session["UserName"];
            var userid = c.Users.Where(x => x.UserName == username).
                Select(y => y.UserID).FirstOrDefault();
            p.UserID = userid;
            favorite.Add(p);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFavorite(int id, string username)
        {
            var favorites = favorite.GetList();
            FavoriteSerie p = new FavoriteSerie();
            p.SerieID = id;
            username = (string)Session["UserName"];
            var userid = c.Users.Where(x => x.UserName == username).
                Select(y => y.UserID).FirstOrDefault();
            p.UserID = userid;
            favorites = favorites.FindAll(x => x.SerieID == id);
            favorites = favorites.FindAll(x => x.UserID == userid);
            favorite.DeleteById(favorites.First().ID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult DeleteFavorite(int id, string username)
        //{
        //    var favorites = favorite.GetList();
        //    FavoriteSerie p = new FavoriteSerie();
        //    p.SerieID = id;
        //    username = (string)Session["UserName"];
        //    var userid = c.Users.Where(x => x.UserName == username).
        //        Select(y => y.UserID).FirstOrDefault();
        //    p.UserID = userid;
        //    favorites = favorites.FindAll(x => x.SerieID == id);
        //    favorites = favorites.FindAll(x => x.UserID == userid);
        //    favorite.DeleteById(favorites.First().ID);
        //    return RedirectToAction("MarvelUniverse", "Universe");
        //}

        public PartialViewResult SerieComments(int id)
        {
            var comments = seriecomment.GetListByMovie(id);
            comments = comments.Where(x => x.CommentStatus == true).ToList();
            return PartialView(comments);
        }

        [HttpGet]
        public PartialViewResult WriteComment(int id)
        {
            ViewBag.serieid = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult WriteComment(SerieComment p)
        {
            p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            int userid = (int)Session["UserID"];
            p.UserID = userid;
            p.CommentStatus = true;
            if(p.SerieCommentText.Length > 0)
            {
                seriecomment.Add(p);
            }
            var serieinfo = serie.GetByID(p.ID);
            int serieid = serieinfo.ID;
            return RedirectToAction("Serie", new { id = serieid });
        }

        public JsonResult LikeComment(int id)
        {
            var comment = seriecomment.GetByID(id);
            int serieid = comment.ID;
            var reacts = sericomreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.SerieCommentID == id && x.UserId == userid);
            if (reacts.Count() != 0)
            {
                var react = reacts.First();
                if (react.Reaction == "Dislike")
                {
                    comment.DisLike -= 1;
                }
                react.Reaction = "Like";
                sericomreact.Update(react);
            }
            else
            {
                SerieCommentReaction p = new SerieCommentReaction();
                p.SerieCommentID = id;
                p.UserId = userid;
                p.Reaction = "Like";
                sericomreact.Add(p);
            }
            comment.Like = comment.Like + 1;
            seriecomment.Update(comment);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Serie", new { id = serieid });
        }
        public JsonResult DisLikeComment(int id)
        {
            var comment = seriecomment.GetByID(id);
            int serieid = comment.ID;
            var reacts = sericomreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.SerieCommentID == id && x.UserId == userid);
            if (reacts.Count() != 0)
            {
                var react = reacts.First();
                if (react.Reaction == "Like")
                {
                    comment.Like -= 1;
                }
                react.Reaction = "Dislike";
                sericomreact.Update(react);
            }
            else
            {
                SerieCommentReaction p = new SerieCommentReaction();
                p.SerieCommentID = id;
                p.UserId = userid;
                p.Reaction = "Dislike";
                sericomreact.Add(p);
            }
            comment.DisLike = comment.DisLike + 1;
            seriecomment.Update(comment);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Serie", new { id = serieid });
        }
        public JsonResult DeleteReaction(int id)
        {
            var comment = seriecomment.GetByID(id);
            int serieid = comment.ID;
            var reacts = sericomreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.SerieCommentID == id && x.UserId == userid);
            var react = reacts.First();
            if (react.Reaction == "Like")
            {
                comment.Like -= 1;
            }
            else if (react.Reaction == "Dislike")
            {
                comment.DisLike -= 1;
            }
            sericomreact.Delete(react);
            seriecomment.Update(comment);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Serie", new { id = serieid });
        }

        [HttpGet]
        public PartialViewResult AnswerComment(int id)
        {
            ViewBag.SerieCommentID = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AnswerComment(SerieCommentAnswer p)
        {
            int sericeommentid = p.SerieCommentID;
            var comment = seriecomment.GetByID(sericeommentid);
            int serieid = comment.ID;
            int userid = (int)Session["UserID"];
            p.UserId = userid;
            p.AnswerDate = DateTime.Parse(DateTime.Now.ToLongTimeString());
            p.AnswerStatus = true;
            sericomans.Add(p);
            return RedirectToAction("Serie", new { id = serieid });
        }

        public JsonResult LikeCommentAnswer(int id)
        {
            var answer = sericomans.GetByID(id);
            int serieid = answer.SerieComment.ID;
            var reacts = sericomansreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.AnswerID == id && x.UserID == userid);
            if (reacts.Count() != 0)
            {
                var react = reacts.First();
                if (react.Reaction == "Dislike")
                {
                    answer.DisLike -= 1;
                }
                react.Reaction = "Like";
                sericomansreact.Update(react);
            }
            else
            {
                SerieCommentAnswerReaction p = new SerieCommentAnswerReaction();
                p.AnswerID = id;
                p.UserID = userid;
                p.Reaction = "Like";
                sericomansreact.Add(p);
            }
            answer.Like = answer.Like + 1;
            sericomans.Update(answer);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Serie", new { id = serieid });
        }
        public JsonResult DisLikeCommentAnswer(int id)
        {
            var answer = sericomans.GetByID(id);
            int serieid = answer.SerieComment.ID;
            var reacts = sericomansreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.AnswerID == id && x.UserID == userid);
            if (reacts.Count() != 0)
            {
                var react = reacts.First();
                if (react.Reaction == "Like")
                {
                    answer.Like -= 1;
                }
                react.Reaction = "Dislike";
                sericomansreact.Update(react);
            }
            else
            {
                SerieCommentAnswerReaction p = new SerieCommentAnswerReaction();
                p.AnswerID = id;
                p.UserID = userid;
                p.Reaction = "DisLike";
                sericomansreact.Add(p);
            }
            answer.DisLike = answer.DisLike + 1;
            sericomans.Update(answer);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Serie", new { id = serieid });
        }
        public JsonResult DeleteCommentAnswerReaction(int id)
        {
            var answer = sericomans.GetByID(id);
            int serieid = answer.SerieComment.ID;
            var reacts = sericomansreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.AnswerID == id && x.UserID == userid);
            var react = reacts.First();
            if (react.Reaction == "Like")
            {
                answer.Like -= 1;
            }
            else if (react.Reaction == "DisLike")
            {
                answer.DisLike -= 1;
            }
            sericomansreact.Delete(react);
            sericomans.Update(answer);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Serie", new { id = serieid });
        }
        
    }
}