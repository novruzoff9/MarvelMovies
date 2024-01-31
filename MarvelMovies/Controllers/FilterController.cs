using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MarvelMovies.Controllers
{
    public class FilterController : Controller
    {
        MovieManager movie = new MovieManager(new EfMovieDal());
        SerieManager serie = new SerieManager(new EfSerieDal());
        AnimationManager animation = new AnimationManager(new EfAnimationDal());
        MovieCommentManager moviecomment = new MovieCommentManager(new EfMovieCommentDal());
        SerieCommentManager seriecomment = new SerieCommentManager(new EfSerieCommentDal());
        AnimationCommentManager animationcomment = new AnimationCommentManager(new EfAnimationCommentDal());
        FavoriteManager favorite = new FavoriteManager(new EfFavoritesDal());
        Context c = new Context();
        // GET: Filter
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Filters()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult FilterResults()
        {
            var movies = movie.GetList();
            ViewBag.sort = Request.Form["sortby"];
            ViewBag.quantity = Request.Form["quantity"];
            try
            {
                string universe = "";
                var nowmovies = movies;
                int count = movies.Count();
                foreach (var item in Request.Form["universe"])
                {
                    if (item.ToString() == ",")
                    {
                        var universemovies = nowmovies.FindAll(x => x.Universe == universe);
                        movies.AddRange(universemovies);
                        universe = "";
                    }
                    else { universe += item.ToString(); }
                }
                var lastuniversemovies = nowmovies.FindAll(x => x.Universe == universe);
                movies.AddRange(lastuniversemovies);
                movies.RemoveRange(0, count);
                ViewBag.universe = universe;
            }
            catch
            { ViewBag.universe = "all"; }

            try
            {
                string producer = "";
                var nowmovies = movies;
                int count = movies.Count();
                foreach (var item in Request.Form["producer"])
                {
                    if (item.ToString() == ",")
                    {
                        var producermovies = nowmovies.FindAll(x => x.Producer == producer);
                        movies.AddRange(producermovies);
                        producer = "";
                    }
                    else { producer += item.ToString(); }
                }
                var lastproducermovies = nowmovies.FindAll(x => x.Producer == producer);
                movies.AddRange(lastproducermovies);
                movies.RemoveRange(0, count);
                ViewBag.producer = producer;
            }
            catch { ViewBag.producer = "all"; }
            decimal minimdb = Request.Form["imdb1"].Replace(".", ",").AsDecimal();
            decimal maximdb = Request.Form["imdb2"].Replace(".", ",").AsDecimal();
            movies = movies.FindAll(x => x.IMDB.Replace(".", ",").Substring(0, 3).AsDecimal() >= minimdb);
            movies = movies.FindAll(x => x.IMDB.Replace(".", ",").Substring(0, 3).AsDecimal() <= maximdb);
            ViewBag.imdb1 = Request.Form["imdb1"];
            ViewBag.imdb2 = Request.Form["imdb2"];
            int minyear = Request.Form["date1"].AsInt();
            int maxyear = Request.Form["date2"].AsInt();
            movies = movies.FindAll(x => x.ReleaseDate.Year >= minyear);
            movies = movies.FindAll(x => x.ReleaseDate.Year <= maxyear);
            ViewBag.date1 = Request.Form["date1"];
            ViewBag.date2 = Request.Form["date2"];
            ViewBag.count = movies.Count();
            string sort = Request.Form["sortby"];
            if (sort == "Date")
            {
                movies = movies.OrderBy(x => x.ReleaseDate).ToList();
            }
            else if (sort == "Alphabet")
            {
                movies = movies.OrderBy(x => x.Name).ToList();
            }
            else if (sort == "Score")
            {
                movies = movies.OrderBy(x => x.IMDB.Replace(".", ",").Substring(0, 3)).ToList();
            }
            else if (sort == "Time")
            {
                movies = movies.OrderBy(x => x.Duration).ToList();
            }
            string quantity = Request.Form["quantity"];
            if (quantity == "decrase")
            {
                movies = Enumerable.Reverse(movies).ToList();
            }
            return View();
        }
    
        public PartialViewResult FilterResultMovies()
        {
            var movies = movie.GetList();

            ViewBag.sort = Request.Form["sortby"];
            ViewBag.quantity = Request.Form["quantity"];
            try
            {
                string universe = "";
                var nowmovies = movies;
                int count = movies.Count();
                foreach (var item in Request.Form["universe"])
                {
                    if (item.ToString() == ",")
                    {
                        var universemovies = nowmovies.FindAll(x => x.Universe == universe);
                        movies.AddRange(universemovies);
                        universe = "";
                    }
                    else { universe += item.ToString(); }
                }
                var lastuniversemovies = nowmovies.FindAll(x => x.Universe == universe);
                movies.AddRange(lastuniversemovies);
                movies.RemoveRange(0, count);
                ViewBag.universe = universe;
            }
            catch
            { ViewBag.universe = "all"; }

            try
            {
                string producer = "";
                var nowmovies = movies;
                int count = movies.Count();
                foreach (var item in Request.Form["producer"])
                {
                    if (item.ToString() == ",")
                    {
                        var producermovies = nowmovies.FindAll(x => x.Producer == producer);
                        movies.AddRange(producermovies);
                        producer = "";
                    }
                    else { producer += item.ToString(); }
                }
                var lastproducermovies = nowmovies.FindAll(x => x.Producer == producer);
                movies.AddRange(lastproducermovies);
                movies.RemoveRange(0, count);
                ViewBag.producer = producer;
            }
            catch { ViewBag.producer = "all"; }
            decimal minimdb = Request.Form["imdb1"].Replace(".", ",").AsDecimal();
            decimal maximdb = Request.Form["imdb2"].Replace(".", ",").AsDecimal();
            movies = movies.FindAll(x => x.IMDB.Replace(".", ",").Substring(0, 3).AsDecimal() >= minimdb);
            movies = movies.FindAll(x => x.IMDB.Replace(".", ",").Substring(0, 3).AsDecimal() <= maximdb);
            ViewBag.imdb1 = Request.Form["imdb1"];
            ViewBag.imdb2 = Request.Form["imdb2"];
            int minyear = Request.Form["date1"].AsInt();
            int maxyear = Request.Form["date2"].AsInt();
            movies = movies.FindAll(x => x.ReleaseDate.Year >= minyear);
            movies = movies.FindAll(x => x.ReleaseDate.Year <= maxyear);
            ViewBag.date1 = Request.Form["date1"];
            ViewBag.date2 = Request.Form["date2"];
            ViewBag.count = movies.Count();
            string sort = Request.Form["sortby"];
            if (sort == "Date")
            {
                movies = movies.OrderBy(x => x.ReleaseDate).ToList();
            }
            else if (sort == "Alphabet")
            {
                movies = movies.OrderBy(x => x.Name).ToList();
            }
            else if (sort == "Score")
            {
                movies = movies.OrderBy(x => x.IMDB.Replace(".", ",").Substring(0, 3)).ToList();
            }
            else if (sort == "Time")
            {
                movies = movies.OrderBy(x => x.Duration).ToList();
            }
            string quantity = Request.Form["quantity"];
            if (quantity == "decrase")
            {
                movies = Enumerable.Reverse(movies).ToList();
            }
            return PartialView(movies);
        }

        public PartialViewResult FilterResultSeries()
        {
            var series = serie.GetList();

            ViewBag.sort = Request.Form["sortby"];
            ViewBag.quantity = Request.Form["quantity"];
            try
            {
                string universe = "";
                var nowseries = series;
                int count = series.Count();
                foreach (var item in Request.Form["universe"])
                {
                    if (item.ToString() == ",")
                    {
                        var universemovies = nowseries.FindAll(x => x.Universe == universe);
                        series.AddRange(universemovies);
                        universe = "";
                    }
                    else { universe += item.ToString(); }
                }
                var lastuniversemovies = nowseries.FindAll(x => x.Universe == universe);
                series.AddRange(lastuniversemovies);
                series.RemoveRange(0, count);
                ViewBag.universe = universe;
            }
            catch
            { ViewBag.universe = "all"; }

            try
            {
                string producer = "";
                var nowseries = series;
                int count = series.Count();
                foreach (var item in Request.Form["producer"])
                {
                    if (item.ToString() == ",")
                    {
                        var producermovies = nowseries.FindAll(x => x.Producer == producer);
                        series.AddRange(producermovies);
                        producer = "";
                    }
                    else { producer += item.ToString(); }
                }
                var lastproducermovies = nowseries.FindAll(x => x.Producer == producer);
                series.AddRange(lastproducermovies);
                series.RemoveRange(0, count);
                ViewBag.producer = producer;
            }
            catch { ViewBag.producer = "all"; }
            decimal minimdb = Request.Form["imdb1"].Replace(".", ",").AsDecimal();
            decimal maximdb = Request.Form["imdb2"].Replace(".", ",").AsDecimal();
            series = series.FindAll(x => x.IMDB.Replace(".", ",").Substring(0, 3).AsDecimal() >= minimdb);
            series = series.FindAll(x => x.IMDB.Replace(".", ",").Substring(0, 3).AsDecimal() <= maximdb);
            ViewBag.imdb1 = Request.Form["imdb1"];
            ViewBag.imdb2 = Request.Form["imdb2"];
            int minyear = Request.Form["date1"].AsInt();
            int maxyear = Request.Form["date2"].AsInt();
            series = series.FindAll(x => x.ReleaseDate.Year >= minyear);
            series = series.FindAll(x => x.ReleaseDate.Year <= maxyear);
            ViewBag.date1 = Request.Form["date1"];
            ViewBag.date2 = Request.Form["date2"];
            ViewBag.count = series.Count();
            string sort = Request.Form["sortby"];
            if (sort == "Date")
            {
                series = series.OrderBy(x => x.ReleaseDate).ToList();
            }
            else if (sort == "Alphabet")
            {
                series = series.OrderBy(x => x.Name).ToList();
            }
            else if (sort == "Score")
            {
                series = series.OrderBy(x => x.IMDB.Replace(".", ",").Substring(0, 3)).ToList();
            }
            else if (sort == "Time")
            {
                series = series.OrderBy(x => x.Duration).ToList();
            }
            string quantity = Request.Form["quantity"];
            if (quantity == "decrase")
            {
                series = Enumerable.Reverse(series).ToList();
            }
            return PartialView(series);
        }
    }
}