using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using MarvelMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarvelMovies.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        UserManager user = new UserManager(new EfUserDal());
        MovieCommentManager movicom = new MovieCommentManager(new EfMovieCommentDal());
        SerieCommentManager sericom = new SerieCommentManager(new EfSerieCommentDal());
        MovieCommentAnswerManager movicomans = new MovieCommentAnswerManager(new EfMovieCommentAnswerDal());
        SerieCommentAnswerManager sericomans = new SerieCommentAnswerManager(new EfSerieCommentAnswerDal());
        MovieManager MovieM = new MovieManager(new EfMovieDal());
        SerieManager SerieM = new SerieManager(new EfSerieDal());
        FavoriteManager favorites = new FavoriteManager(new EfFavoritesDal());
        FavoriteSerieManager favoriteseries = new FavoriteSerieManager(new EfFavoriteSerieDal());
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var users = user.GetList();
            return View(users);
        }

        public ActionResult UserDetails(int id)
        {
            var userdata = user.GetById(id);
            var moviecomcount = (int)c.MovieComments.Where(x => x.UserID == id).Count();
            var seriecomcount = (int)c.SerieComments.Where(x => x.UserID == id).Count();
            var comments = moviecomcount + seriecomcount;
            ViewBag.commentscount = comments;
            int favmoviecount = c.Favorites.Where(x => x.UserID == id).Count();
            int favseriecount = c.FavoriteSeries.Where(x => x.UserID == id).Count();
            int favorites = favmoviecount + favseriecount;
            ViewBag.favoritescount = favorites;
            return View(userdata);
        }

        public ActionResult AllFavorites(int id)
        {
            ViewBag.userid = id;
            return View();
        }

        public PartialViewResult FavoriteMovies(int id)
        {
            var userinfo = user.GetById(id);
            var favoritemovies = favorites.GetList();
            favoritemovies = favoritemovies.FindAll(x => x.UserID == id).ToList();
            List<Movie> movies = new List<Movie>();
            foreach (var item in favoritemovies)
            {
                var movie = MovieM.GetByID(item.MovieID);
                movies.Add(movie);
            }
            return PartialView(movies);
        }
        public PartialViewResult FavoriteSeries(int id)
        {
            var userinfo = user.GetById(id);
            var favoriteserielist = favoriteseries.GetList();
            favoriteserielist = favoriteserielist.FindAll(x => x.UserID == id).ToList();
            List<Serie> series = new List<Serie>();
            foreach (var item in favoriteserielist)
            {
                var serie = SerieM.GetByID(item.SerieID);
                series.Add(serie);
            }
            return PartialView(series);
        }

        public ActionResult CommentByUser(int id)
        {
            ViewBag.id = id;
            ViewBag.username = user.GetById(id).UserName;
            return View();
        }

        public PartialViewResult CommentPercentage(int id)
        {
            ViewBag.userid = id;
            return PartialView();
        }

        public ActionResult CommentPercentageChart(int id)
        {
            return Json(CommentsPercentage(id), JsonRequestBehavior.AllowGet);
        }

        public List<CommentsCount> CommentsPercentage(int id)
        {
            List<CommentsCount> cc = new List<CommentsCount>();
            cc.Add(new CommentsCount()
            {
                Name = "Film Rəyləri",
                Count = c.MovieComments.Where(x=>x.UserID == id).Count()
            });
            cc.Add(new CommentsCount()
            {
                Name = "Serial Rəyləri",
                Count = c.SerieComments.Where(x => x.UserID == id).Count()
            });
            cc.Add(new CommentsCount()
            {
                Name = "Film Rəyi Cavabları",
                Count = c.MovieCommentAnswers.Where(x => x.UserId == id).Count()
            });
            cc.Add(new CommentsCount()
            {
                Name = "Serial Rəyi Cavabları",
                Count = c.SerieCommentAnswers.Where(x => x.UserId == id).Count()
            });
            return cc;
        }

        public PartialViewResult CommentCount(int id)
        {
            ViewBag.userid = id;
            return PartialView();
        }

        public ActionResult CommentChart(int id)
        {
            return Json(DailyCommentCount(id), JsonRequestBehavior.AllowGet);
        }

        public List<DailyItemCount> DailyCommentCount(int id)
        {
            List<DailyItemCount> cc = new List<DailyItemCount>();
            var userinfo = user.GetById(id);
            DateTime date = userinfo.UserSignDate;
            var moviecomments = movicom.GetListByUser(id);
            var seriecomments = sericom.GetListByUser(id);
            var movieans = movicomans.GetList();
            movieans = movieans.Where(x => x.UserId == id).ToList();
            var serieans = sericomans.GetList();
            serieans = serieans.Where(x => x.UserId == id).ToList();
            while (date < DateTime.Now)
            {
                    int mcc = moviecomments.Where(x => x.CommentDate.ToShortDateString() == date.ToShortDateString()).Count();
                    int scc = seriecomments.Where(x => x.CommentDate.ToShortDateString() == date.ToShortDateString()).Count();
                    int mac = movieans.Where(x => x.AnswerDate.ToShortDateString() == date.ToShortDateString()).Count();
                    int sac = serieans.Where(x => x.AnswerDate.ToShortDateString() == date.ToShortDateString()).Count();
                    cc.Add(new DailyItemCount()
                    {
                        Date = date.ToString("dd MMMM yyyy"),
                        Item1Count = mcc + scc + mac + sac
                    });
                date =  date.AddDays(1);
            }
            return cc;
        }
    }
}