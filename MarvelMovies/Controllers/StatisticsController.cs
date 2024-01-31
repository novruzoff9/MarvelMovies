using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using MarvelMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarvelMovies.Controllers
{
    public class StatisticsController : Controller
    {
        UserManager user = new UserManager(new EfUserDal());
        MovieManager movie = new MovieManager(new EfMovieDal());
        SerieManager serie = new SerieManager(new EfSerieDal());
        MovieCommentManager moviecom = new MovieCommentManager(new EfMovieCommentDal());
        SerieCommentManager seriecom = new SerieCommentManager(new EfSerieCommentDal());
        MovieCommentAnswerManager moviecomans = new MovieCommentAnswerManager(new EfMovieCommentAnswerDal());
        SerieCommentAnswerManager seriecomans = new SerieCommentAnswerManager(new EfSerieCommentAnswerDal());
        Context c = new Context();
        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            return View();
        }

        public PartialViewResult MoviePercentage()
        {
            return PartialView();
        }

        public PartialViewResult CommentPercentage()
        {
            return PartialView();
        }

        public PartialViewResult MovieCommentsCount()
        {
            return PartialView();
        }

        public PartialViewResult SerieCommentsCount()
        {
            return PartialView();
        }

        public PartialViewResult DailyUserCount()
        {
            return PartialView();
        }

        public ActionResult MovieChart()
        {
            return Json(ProductsCount(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommentChart()
        {
            return Json(CommentsCount(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommentCountBar()
        {
            return Json(MoviesCommentCount(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CommentCountBar2()
        {
            return Json(SeriesCommentCount(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyUserSignLineChart()
        {
            return Json(DailyUserSignCount(), JsonRequestBehavior.AllowGet);
        }

        public List<ProductsCount> ProductsCount()
        {
            List<ProductsCount> pc = new List<ProductsCount>();
            pc.Add(new ProductsCount()
            {
                Name = "Film",
                Count = c.Movies.Count()
            });
            pc.Add(new ProductsCount()
            {
                Name = "Serial",
                Count = c.Series.Count()
            });
            return pc;
        }

        public List<CommentsCount> CommentsCount()
        {
            List<CommentsCount> cc = new List<CommentsCount>();
            cc.Add(new CommentsCount()
            {
                Name = "Film Rəyləri",
                Count = c.MovieComments.Count()
            });
            cc.Add(new CommentsCount()
            {
                Name = "Serial Rəyləri",
                Count = c.SerieComments.Count()
            });
            //cc.Add(new CommentsCount()
            //{
            //    Name = "Animasiya Rəyləri",
            //    Count = c.AnimationComments.Count()
            //});
            cc.Add(new CommentsCount()
            {
                Name = "Film Rəyi Cavabları",
                Count = c.MovieCommentAnswers.Count()
            });
            cc.Add(new CommentsCount()
            {
                Name = "Serial Rəyi Cavabları",
                Count = c.SerieCommentAnswers.Count()
            });
            return cc;
        }

        public List<MoviesCommentCount> MoviesCommentCount()
        {
            List<MoviesCommentCount> mcc = new List<MoviesCommentCount>();
            List<MoviesCommentCount> films = new List<MoviesCommentCount>();
            var movies = movie.GetList();
            var comments = moviecom.GetList();
            var answers = moviecomans.GetList();
            foreach (var item in movies)
            {
                mcc.Add(new MoviesCommentCount()
                {
                    MovieName = item.Name,
                    MovieComment = comments.Where(x => x.ID == item.ID).Count(),
                    AnswerComment = answers.Where(x => x.MovieComment.ID == item.ID).Count(),
                    CommentCount = comments.Where(x => x.ID == item.ID).Count() +
                    answers.Where(x => x.MovieComment.ID == item.ID).Count()
                });
            }
            mcc = mcc.OrderBy(x => x.CommentCount).ToList();
            mcc = Enumerable.Reverse(mcc).ToList();
            films = mcc.Take(5).ToList();
            return films;
        }
        public List<MoviesCommentCount> SeriesCommentCount()
        {
            List<MoviesCommentCount> scc = new List<MoviesCommentCount>();
            List<MoviesCommentCount> top5series = new List<MoviesCommentCount>();
            var series = serie.GetList();
            var comments = seriecom.GetList();
            var answers = seriecomans.GetList();
            foreach (var item in series)
            {
                scc.Add(new MoviesCommentCount()
                {
                    MovieName = item.Name,
                    MovieComment = comments.Where(x => x.ID == item.ID).Count(),
                    AnswerComment = answers.Where(x => x.SerieComment.ID == item.ID).Count(),
                    CommentCount = comments.Where(x => x.ID == item.ID).Count() +
                    answers.Where(x => x.SerieComment.ID == item.ID).Count()
                });
            }
            scc = scc.OrderBy(x => x.CommentCount).ToList();
            scc = Enumerable.Reverse(scc).ToList();
            top5series = scc.Take(5).ToList();
            return top5series;
        }
        public List<DailyUserSignUp> DailyUserSignCount()
        {
            List<DailyUserSignUp> us = new List<DailyUserSignUp>();
            var users = user.GetList();
            foreach (var item in users)
            {
                if (!us.Any(x=>x.Date == (((DateTime)item.UserSignDate).ToString("dd MMMM yyyy"))))
                {
                    us.Add(new DailyUserSignUp()
                    {
                        Date = (((DateTime)item.UserSignDate).ToString("dd MMMM yyyy")),
                        SignedUsersCount = users.Where(x => x.UserSignDate == item.UserSignDate).Count()
                    });
                }
            }
            return us;
        }
    }
}