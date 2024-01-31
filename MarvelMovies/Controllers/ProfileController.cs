using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
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
    public class ProfileController : Controller
    {
        // GET: Profile
        MovieCommentManager moviecom = new MovieCommentManager(new EfMovieCommentDal());
        SerieCommentManager seriecom = new SerieCommentManager(new EfSerieCommentDal());
        UserManager user = new UserManager(new EfUserDal());
        MovieManager MovieM = new MovieManager(new EfMovieDal());
        SerieManager SerieM = new SerieManager(new EfSerieDal());
        FavoriteManager favorites = new FavoriteManager(new EfFavoritesDal());
        FavoriteSerieManager favoriteseries = new FavoriteSerieManager(new EfFavoriteSerieDal());
        Context c = new Context();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyProfile(string p)
        {
            p = (string)Session["UserName"];
            var userid = c.Users.Where(x => x.UserName == p).
                Select(y => y.UserID).FirstOrDefault();
            var userinfo = user.GetById(userid);
            var moviecomcount = (int)c.MovieComments.Where(x => x.UserID == userid).Count();
            var seriecomcount = (int)c.SerieComments.Where(x => x.UserID == userid).Count();
            var comments = moviecomcount + seriecomcount;
            ViewBag.commentscount = comments;
            int favmoviecount = c.Favorites.Where(x => x.UserID == userid).Count();
            int favseriecount = c.FavoriteSeries.Where(x => x.UserID == userid).Count();
            int favorites = favmoviecount + favseriecount;
            ViewBag.favoritescount = favorites;
            return View(userinfo);
        }

        public ActionResult UserProfile(int id)
        {
            var userinfo = user.GetById(id);            
            try
            {
                var userid = (int)Session["UserID"];
                if (id == userid)
                {
                    return RedirectToAction("MyProfile");
                }
            }
            catch (Exception){    }            
            var moviecomcount = (int)c.MovieComments.Where(x => x.UserID == id).Count();
            var seriecomcount = (int)c.SerieComments.Where(x => x.UserID == id).Count();
            var comments = moviecomcount + seriecomcount;
            ViewBag.commentscount = comments;
            var favmoviecount = (int)c.Favorites.Where(x => x.UserID == id).Count();
            //var favseriecount = (int)c.SerieComments.Where(x => x.UserID == userid).Count();
            var favorites = favmoviecount;
            ViewBag.favoritescount = favorites;
            return View(userinfo);
        }

        [HttpGet]
        public ActionResult UpdateProfile(string p)
        {
            p = (string)Session["UserName"];
            var userid = c.Users.Where(x => x.UserName == p).
                Select(y => y.UserID).FirstOrDefault();
            var userinfo = user.GetById(userid);
            var moviecomcount = (int)c.MovieComments.Where(x => x.UserID == userid).Count();
            var seriecomcount = (int)c.SerieComments.Where(x => x.UserID == userid).Count();
            var comments = moviecomcount + seriecomcount;
            ViewBag.commentscount = comments;
            var favmoviecount = (int)c.Favorites.Where(x => x.UserID == userid).Count();
            //var favseriecount = (int)c.SerieComments.Where(x => x.UserID == userid).Count();
            var favorites = favmoviecount;
            ViewBag.favoritescount = favorites;
            return View(userinfo);
        }

        [HttpPost]
        public ActionResult UpdateProfile(User p)
        {
            string username = (string)Session["UserName"];
            var userinfo = c.Users.Where(x => x.UserName == username).
                FirstOrDefault();
            p.UserName = userinfo.UserName;
            p.UserSignDate = userinfo.UserSignDate;
            p.UserRole = userinfo.UserRole;
            try
            {
                if (Request.Files.Count > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string adress = "~/Images/UserProfilePhotos/" + filename;
                    Request.Files[0].SaveAs(Server.MapPath(adress));
                    p.UserImage = "/Images/UserProfilePhotos/" + filename;
                }
            }
            catch (Exception)
            {
                p.UserImage = userinfo.UserImage;
            }           
            user.UserUpdate(p);
            return RedirectToAction("MyProfile");
        }

        public PartialViewResult MyComments(string p)
        {
            p = (string)Session["UserName"];
            var userid = c.Users.Where(x => x.UserName == p).
                Select(y => y.UserID).FirstOrDefault();
            ViewBag.id = userid;
            var userinfo = user.GetById(userid);
            var comments = userinfo.Comments;
            var seriecomments = userinfo.SerieComments;
            var moviecomments = moviecom.GetListByUser(userid);
            return PartialView(moviecomments);
        }

        public ActionResult AllComments(string p)
        {
            p = (string)Session["UserName"];
            var id = c.Users.Where(x => x.UserName == p).
                Select(y => y.UserID).FirstOrDefault();
            ViewBag.id = id;
            ViewBag.username = p;
            var userinfo = user.GetById(id);
            var moviecomments = moviecom.GetListByUser(id);
            var seriecomments = seriecom.GetListByUser(id);
            return View(moviecomments);
        }

        public PartialViewResult UserComments(int id)
        {
            var userid = id;
            ViewBag.id = userid;
            var userinfo = user.GetById(userid);
            var comments = userinfo.Comments;
            var seriecomments = userinfo.SerieComments;
            var moviecomments = moviecom.GetListByUser(userid);
            return PartialView(moviecomments);
        }

        [HttpGet]
        public ActionResult UpdateMovieComment(int id)
        {
            var comment = moviecom.GetByID(id);
            return View(comment);
        }
        [HttpPost]
        public ActionResult UpdateMovieComment(MovieComment p)
        {
            p.CommentStatus = true;
            moviecom.MovieCommentUpdate(p);
            return RedirectToAction("Comments");
        }


        public ActionResult DeleteMovieComment(int id)
        {
            var comment = moviecom.GetByID(id);
            comment.CommentStatus = false;
            moviecom.MovieCommentUpdate(comment);
            return RedirectToAction("Comments");
        }

        public PartialViewResult FavoritesList()
        {
            var id = (int)Session["UserID"];
            ViewBag.id = id;
            var userinfo = user.GetById(id);
            var movies = favorites.GetList();
            movies =  movies.FindAll(x => x.UserID == id).ToList();
            return PartialView(movies);
        }

        public PartialViewResult UserFavoritesList(int id)
        {
            ViewBag.id = id;
            var userinfo = user.GetById(id);
            var movies = favorites.GetList();
            movies = movies.FindAll(x => x.UserID == id).ToList();
            return PartialView(movies);
        }


        public ActionResult AllFavorites()
        {
            return View();
        }

        public PartialViewResult FavoriteMovies()
        {
            var id = (int)Session["UserID"];
            ViewBag.id = id;
            var userinfo = user.GetById(id);
            var favoritemovies = favorites.GetList();
            favoritemovies = favoritemovies.FindAll(x => x.UserID == id).ToList();
            List<Movie> movies = new List<Movie>();
            foreach (var item in favoritemovies)
            {
                var movie = MovieM.GetByID(item.MovieID);
                movies.Add(movie);
            }
            movies = movies.OrderBy(x => x.ReleaseDate).ToList();
            return PartialView(movies);
        }
        public PartialViewResult FavoriteSeries()
        {
            var id = (int)Session["UserID"];
            ViewBag.id = id;
            var userinfo = user.GetById(id);
            var favoriteserielist = favoriteseries.GetList();
            favoriteserielist = favoriteserielist.FindAll(x => x.UserID == id).ToList();
            List<Serie> series = new List<Serie>();
            foreach (var item in favoriteserielist)
            {
                var serie = SerieM.GetByID(item.SerieID);
                series.Add(serie);
            }
            series = series.OrderBy(x => x.ReleaseDate).ToList();
            return PartialView(series);
        }

        public ActionResult SearchUser()
        {
            return View();
        }

        public ActionResult SearchResults(string searchuser)
        {
            var users = user.GetList();
            char[] str = searchuser.ToArray();
            for (int i = 0; i < searchuser.Length; i++)
            {
                if (searchuser[i] == 'I')
                {
                    str[i] = 'i';
                }
            }
            string srch = new string(str);
            ViewBag.search = srch.ToLower();
            return PartialView(users);
        }
        public JsonResult Users(string search)
        {
            var users = user.GetList();
            users = users.Where(x => x.UserName.ToLower().Contains(search.ToLower())).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}