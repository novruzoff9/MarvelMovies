using BusinessLayer.Concrete;
using BusinessLayer.Validation_Rules;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using DataAccessLayer.Concrete;

namespace MarvelMovies.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie

        MovieManager movie = new MovieManager(new EfMovieDal());
        MovieValidator MovieValidations = new MovieValidator();
        Context c = new Context();
        
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles ="Main")]
        public ActionResult MoviesList(int page = 1)
        {
            var movies = movie.GetList().ToPagedList(page, 10);
            return View(movies);
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie p)
        {
            p.Status = true;
            ValidationResult results = MovieValidations.Validate(p);
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string adress = "~/Images/MovieImages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(adress));
                p.Image = "/Images/MovieImages/" + filename;
            }
            if (results.IsValid)
            {
                movie.MovieAdd(p);
                return RedirectToAction("MoviesList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult ActiveMovie(int id)
        {
            var movievalue = movie.GetByID(id);
            movievalue.Status = true;
            movie.MovieUpdate(movievalue);
            return RedirectToAction("MoviesList");
        }

        public ActionResult DeleteMovie(int id)
        {
            var movievalue = movie.GetByID(id);
            movievalue.Status = false;
            movie.MovieUpdate(movievalue);
            return RedirectToAction("MoviesList");
        }

        [HttpGet]
        public ActionResult UpdateMovie(int id)
        {
            var movievalue = movie.GetByID(id);
            return View(movievalue);
        }
        [HttpPost]
        public ActionResult UpdateMovie(Movie p)
        {
            var movieinfo = c.Movies.Where(x => x.ID == p.ID).FirstOrDefault();
            try
            {
                if (Request.Files.Count > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string adress = "~/Images/MovieImages/" + filename;
                    Request.Files[0].SaveAs(Server.MapPath(adress));
                    p.Image = "/Images/MovieImages/" + filename;
                }
            }
            catch (Exception)
            {
                p.Image = movieinfo.Image;
            }
            
            //ValidationResult results = MovieValidations.Validate(p);
            //if (results.IsValid)
            //{
            //    movie.MovieUpdate(p);
            //    return RedirectToAction("MoviesList");
            //}
            //else
            //{
            //    foreach (var item in results.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}
            movie.MovieUpdate(p);
            return RedirectToAction("MoviesList");
        }

        public ActionResult MovieDetails(int id)
        {
            var movievalue = movie.GetByID(id);
            return View(movievalue);
        }
    }
}