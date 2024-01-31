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
    public class MovieCommentController : Controller
    {
        // GET: MovieComment
        MovieCommentManager movieComment = new MovieCommentManager(new EfMovieCommentDal());
        MovieManager movie = new MovieManager(new EfMovieDal());
        UserManager user = new UserManager(new EfUserDal());
        MovieCommentAnswerManager movicomans = new MovieCommentAnswerManager(new EfMovieCommentAnswerDal());
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MovieCommentsList()
        {
            var comments = movieComment.GetList();
            return View(comments);
        }

        [HttpGet]
        public ActionResult AddMovieComment()
        {
            List<SelectListItem> valuemovie = (from x in movie.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.ID.ToString()
                                               }).ToList();
            ViewBag.vlm = valuemovie;
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
        public ActionResult AddMovieComment(MovieComment p)
        {
            p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            movieComment.MovieCommentAdd(p);
            return RedirectToAction("MovieCommentsList");
        }

        public ActionResult DelMovieComment(int id)
        {
            var comment = movieComment.GetByID(id);
            comment.CommentStatus = false;
            movieComment.MovieCommentUpdate(comment);
            return RedirectToAction("MovieCommentsList");
        }

        public ActionResult ActiveMovieComment(int id)
        {
            var comment = movieComment.GetByID(id);
            comment.CommentStatus = true;
            movieComment.MovieCommentUpdate(comment);
            return RedirectToAction("MovieCommentsList");
        }

        public ActionResult DelCommentByMovie(int id)
        {
            var comment = movieComment.GetByID(id);
            int movieid = comment.ID;
            comment.CommentStatus = false;
            movieComment.MovieCommentUpdate(comment);
            return RedirectToAction("CommentByMovie", new { id = movieid });
        }

        public ActionResult DelAnswerByMovie(int id)
        {
            var answer = movicomans.GetByID(id);
            int movieid = answer.MovieComment.ID;
            answer.AnswerStatus = false;
            movicomans.Update(answer);
            return RedirectToAction("AnswersByMovie", new { id = movieid });
        }

        public ActionResult ActiveCommentByMovie(int id)
        {
            var comment = movieComment.GetByID(id);
            int movieid = comment.ID;
            comment.CommentStatus = true;
            movieComment.MovieCommentUpdate(comment);
            return RedirectToAction("CommentByMovie", new { id = movieid });
        }

        public ActionResult ActiveAnswerByMovie(int id)
        {
            var answer = movicomans.GetByID(id);
            int movieid = answer.MovieComment.ID;
            answer.AnswerStatus = true;
            movicomans.Update(answer);
            return RedirectToAction("AnswersByMovie", new { id = movieid });
        }

        public ActionResult CommentByMovie(int id)
        {
            ViewBag.id = id;
            ViewBag.name = movie.GetByID(id).Name;
            var comments = movieComment.GetList();
            return View(comments);
        }

        public ActionResult AnswersByMovie(int id)
        {
            ViewBag.id = id;
            ViewBag.name = movie.GetByID(id).Name;
            var answers = movicomans.GetList();
            answers = answers.Where(x => x.MovieComment.ID == id).ToList();
            return View(answers);
        }

        public PartialViewResult MovieCommentsByUser(int id)
        {
            var comments = movieComment.GetList();
            comments = comments.Where(x => x.UserID == id).ToList();
            return PartialView(comments);
        }

        public PartialViewResult MovieCommentAnswersByUser(int id)
        {
            var answers = movicomans.GetList();
            answers = answers.Where(x => x.UserId == id).ToList();
            return PartialView(answers);
        }
    }
}