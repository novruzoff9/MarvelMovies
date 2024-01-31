using BusinessLayer.Concrete;
using BusinessLayer.Validation_Rules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarvelMovies.Controllers
{
    public class SerieController : Controller
    {
        // GET: Serie

        SerieManager serie = new SerieManager(new EfSerieDal());
        MovieValidator MovieValidations = new MovieValidator();
        FavoriteSerieManager Favorites = new FavoriteSerieManager(new EfFavoriteSerieDal());
        Context c = new Context();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SeriesList(int page = 1)
        {
            var series = serie.GetList().ToPagedList(page, 10);
            return View(series);
        }

        [HttpGet]
        public ActionResult AddSerie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSerie(Serie p)
        {
            p.Status = true;
            //ValidationResult results = MovieValidations.Validate(p);
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string adress = "~/Images/SerieImages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(adress));
                p.Image = "/Images/SerieImages/" + filename;
            }
            /*
            if (results.IsValid)
            {
                serie.SerieAdd(p);
                return RedirectToAction("MoviesList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            */
            serie.SerieAdd(p);
            return RedirectToAction("SeriesList");
        }

        public ActionResult ActiveSerie(int id)
        {
            var serievalue = serie.GetByID(id);
            serievalue.Status = true;
            serie.SerieUpdate(serievalue);
            return RedirectToAction("SeriesList");
        }

        public ActionResult DeleteSerie(int id)
        {
            var serievalue = serie.GetByID(id);
            serievalue.Status = false;
            serie.SerieUpdate(serievalue);
            return RedirectToAction("SeriesList");
        }

        [HttpGet]
        public ActionResult UpdateSerie(int id)
        {
            var serievalue = serie.GetByID(id);
            return View(serievalue);
        }
        [HttpPost]
        public ActionResult UpdateSerie(Serie p)
        {
            var serieinfo = c.Series.Where(x => x.ID == p.ID).FirstOrDefault();
            try
            {
                if (Request.Files.Count > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string adress = "~/Images/SerieImages/" + filename;
                    Request.Files[0].SaveAs(Server.MapPath(adress));
                    p.Image = "/Images/SerieImages/" + filename;
                }
            }
            catch (Exception)
            {
                p.Image = serieinfo.Image;
            }
            
            /*
            ValidationResult results = MovieValidations.Validate(p);
            if (results.IsValid)
            {
                movie.MovieUpdate(p);
                return RedirectToAction("MoviesList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            */
            serie.SerieUpdate(p);
            return RedirectToAction("SeriesList");
        }

        public ActionResult SerieDetails(int id)
        {
            var serievalue = serie.GetByID(id);
            return View(serievalue);
        }

        //Gorunum qaldi
        public ActionResult FavoritesBySerie(int id)
        {
            var favoriteseries = Favorites.GetByID(id);
            return View(favoriteseries);
        }
    }
}