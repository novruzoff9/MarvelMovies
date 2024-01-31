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
    public class MovieDetailsController : Controller
    {
        MovieManager movie = new MovieManager(new EfMovieDal());
        MovieCommentManager moviecomment = new MovieCommentManager(new EfMovieCommentDal());
        FavoriteManager favorite = new FavoriteManager(new EfFavoritesDal());
        MovieCommentReactionManager movicomreact = new MovieCommentReactionManager(new EfMovieCommentReactionDal());
        MovieCommentAnswerManager movicomans = new MovieCommentAnswerManager(new EfMovieCommentAnswerDal());
        MovieCommentAnswerReactionManager movicomansreact = new MovieCommentAnswerReactionManager(new EfMovieCommentAnswerReactionDal());
        Context c = new Context();

        // GET: MovieDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movie(int id)
        {
            var movieinfo = movie.GetByID(id);
            var comments = moviecomment.GetList();
            var answers = movicomans.GetList();
            ViewBag.movieid = id;
            int commentcount = moviecomment.GetListByMovie(id).Count();
            commentcount += answers.Where(x => x.MovieComment.ID == id).Count();
            ViewBag.commentcount = commentcount;
            return View(movieinfo);
        }

        public PartialViewResult MovieComments(int id)
        {
            var comments = moviecomment.GetListByMovie(id);
            comments = comments.Where(x => x.CommentStatus == true).ToList();
            return PartialView(comments);
        }

        //bos comment paylasmaq olmasn!!!!
        [HttpGet]
        public PartialViewResult WriteComment(int id)
        {
            ViewBag.movieid = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult WriteComment(MovieComment p)
        {
            p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            int userid = (int)Session["UserID"];
            p.UserID = userid;
            p.CommentStatus = true;
            if (p.MovieCommentText.Length > 0)
            {
                moviecomment.MovieCommentAdd(p);
            }
            var movieinfo = movie.GetByID(p.ID);
            int movieid = movieinfo.ID;
            return RedirectToAction("Movie", new { id = movieid });
        }

        public JsonResult LikeComment(int id)
        {
            var comment = moviecomment.GetByID(id);
            int movieid = comment.ID;
            var reacts = movicomreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.MovieCommentID == id && x.UserId == userid);
            if (reacts.Count() != 0)
            {
                var react = reacts.First();
                if (react.Reaction == "Dislike")
                {
                    comment.DisLike -= 1;
                }
                react.Reaction = "Like";
                movicomreact.Update(react);
            }
            else
            {
                MovieCommentReaction p = new MovieCommentReaction();
                p.MovieCommentID = id;
                p.UserId = userid;
                p.Reaction = "Like";
                movicomreact.Add(p);
            }
            comment.Like = comment.Like + 1;
            moviecomment.MovieCommentUpdate(comment);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Movie", new { id = movieid });
        }
        public JsonResult DisLikeComment(int id)
        {
            var comment = moviecomment.GetByID(id);
            int movieid = comment.ID;
            var reacts = movicomreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.MovieCommentID == id && x.UserId == userid);
            if (reacts.Count() != 0)
            {
                var react = reacts.First();
                if (react.Reaction == "Like")
                {
                    comment.Like -= 1;
                }
                react.Reaction = "Dislike";
                movicomreact.Update(react);
            }
            else
            {
                MovieCommentReaction p = new MovieCommentReaction();
                p.MovieCommentID = id;
                p.UserId = userid;
                p.Reaction = "Dislike";
                movicomreact.Add(p);
            }
            comment.DisLike = comment.DisLike + 1;
            moviecomment.MovieCommentUpdate(comment);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Movie", new { id = movieid });
        }
        public JsonResult DeleteReaction(int id)
        {
            var comment = moviecomment.GetByID(id);
            int movieid = comment.ID;
            var reacts = movicomreact.GetList();
            int userid = (int)Session["UserID"];
            reacts = reacts.FindAll(x => x.MovieCommentID == id && x.UserId == userid);
            var react = reacts.First();
            if (react.Reaction == "Like")
            {
                comment.Like -= 1;
            }
            else if (react.Reaction == "Dislike")
            {
                comment.DisLike -= 1;
            }
            movicomreact.Delete(react);
            moviecomment.MovieCommentUpdate(comment);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Movie", new { id = movieid });
        }

        [HttpGet]
        public PartialViewResult AnswerComment(int id)
        {
            ViewBag.MovieCommentID = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AnswerComment(MovieCommentAnswer p)
        {
            int moviceommentid = p.MovieCommentID;
            var comment = moviecomment.GetByID(moviceommentid);
            int movieid = comment.ID;
            int userid = (int)Session["UserID"];
            p.UserId = userid;
            p.AnswerDate = DateTime.Parse(DateTime.Now.ToLongTimeString());
            p.AnswerStatus = true;
            movicomans.Add(p);
            return RedirectToAction("Movie", new { id = movieid });
        }

        public JsonResult LikeCommentAnswer(int id)
        {
            var answer = movicomans.GetByID(id);
            int movieid = answer.MovieComment.ID;
            var reacts = movicomansreact.GetList();
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
                movicomansreact.Update(react);
            }
            else
            {
                MovieCommentAnswerReaction p = new MovieCommentAnswerReaction();
                p.AnswerID = id;
                p.UserID = userid;
                p.Reaction = "Like";
                movicomansreact.Add(p);
            }
            answer.Like = answer.Like + 1;
            movicomans.Update(answer);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Movie", new { id = movieid });
        }
        public JsonResult DisLikeCommentAnswer(int id)
        {
            var answer = movicomans.GetByID(id);
            int movieid = answer.MovieComment.ID;
            var reacts = movicomansreact.GetList();
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
                movicomansreact.Update(react);
            }
            else
            {
                MovieCommentAnswerReaction p = new MovieCommentAnswerReaction();
                p.AnswerID = id;
                p.UserID = userid;
                p.Reaction = "DisLike";
                movicomansreact.Add(p);
            }
            answer.DisLike = answer.DisLike + 1;
            movicomans.Update(answer);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Movie", new { id = movieid });
        }
        public JsonResult DeleteCommentAnswerReaction(int id)
        {
            var answer = movicomans.GetByID(id);
            int movieid = answer.MovieComment.ID;
            var reacts = movicomansreact.GetList();
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
            movicomansreact.Delete(react);
            movicomans.Update(answer);
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Movie", new { id = movieid });
        }

        //public ActionResult AddFavorite(int id, string username)
        //{
        //    Favorites p = new Favorites();
        //    p.MovieID = id;
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
            Favorites p = new Favorites();
            p.MovieID = id;
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
            Favorites p = new Favorites();
            p.MovieID = id;
            username = (string)Session["UserName"];
            var userid = c.Users.Where(x => x.UserName == username).
                Select(y => y.UserID).FirstOrDefault();
            p.UserID = userid;
            favorites = favorites.FindAll(x => x.MovieID == id);
            favorites = favorites.FindAll(x => x.UserID == userid);
            favorite.DeleteById(favorites.First().ID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult DeleteFavorite(int id, string username)
        //{
        //    var favorites = favorite.GetList();
        //    Favorites p = new Favorites();
        //    p.MovieID = id;
        //    username = (string)Session["UserName"];
        //    var userid = c.Users.Where(x => x.UserName == username).
        //        Select(y => y.UserID).FirstOrDefault();
        //    p.UserID = userid;
        //    favorites = favorites.FindAll(x => x.MovieID == id);
        //    favorites = favorites.FindAll(x => x.UserID == userid);
        //    favorite.DeleteById(favorites.First().ID);
        //    return RedirectToAction("MarvelUniverse", "Universe");
        //}
    }
}