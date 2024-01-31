using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MarvelMovies.Controllers
{
    public class UniverseController : Controller
    {
        MovieManager movie = new MovieManager(new EfMovieDal());
        SerieManager serie = new SerieManager(new EfSerieDal());
        MovieCommentManager moviecomment = new MovieCommentManager(new EfMovieCommentDal());
        SerieCommentManager seriecomment = new SerieCommentManager(new EfSerieCommentDal());
        FavoriteManager favorite = new FavoriteManager(new EfFavoritesDal());
        MovieCommentReactionManager movicomreact = new MovieCommentReactionManager(new EfMovieCommentReactionDal());
        MovieCommentAnswerManager movicomans = new MovieCommentAnswerManager(new EfMovieCommentAnswerDal());
        MovieCommentAnswerReactionManager movicomansreact = new MovieCommentAnswerReactionManager(new EfMovieCommentAnswerReactionDal());
        Context c = new Context();
        // GET: Universes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MarvelUniverse()
        {
            return View();
        }

        public ActionResult XMenUniverse()
        {
            return View();
        }

        public ActionResult SonyUniverse()
        {
            return View();
        }

        public ActionResult OtherUniverse()
        {
            return View();
        }

        public PartialViewResult UniverseMovies(string universe)
        {
            var movies = movie.GetList();
            movies = movies.Where(x => x.Universe == universe).ToList();
            return PartialView(movies);
        }
        public PartialViewResult UniverseSeries(string universe)
        {
            var series = serie.GetList();
            series = series.Where(x => x.Universe == universe).ToList();
            return PartialView(series);
        }

        public JsonResult Movies()
        {
            var movies = movie.GetList();
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Series()
        {
            var series = serie.GetList();
            return Json(series, JsonRequestBehavior.AllowGet);
        }    

        public ActionResult Search()
        {
            return View();
        }

        public PartialViewResult SearchMovies(string search)
        {
            var movies = movie.GetList();
            char[] str = search.ToArray();
            for (int i = 0; i < search.Length; i++)
            {
                if (search[i] == 'I')
                {
                    str[i] = 'i';
                }
            }
            string srch = new string(str);
            ViewBag.search = srch.ToLower();
            return PartialView(movies);
        }

        public PartialViewResult SearchSeries(string search)
        {
            var series = serie.GetList();
            char[] str = search.ToArray();
            for (int i = 0; i < search.Length; i++)
            {
                if (search[i] == 'I')
                {
                    str[i] = 'i';
                }
            }
            string srch = new string(str);
            ViewBag.search = srch.ToLower();
            return PartialView(series);
        }

    }
}