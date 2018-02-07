using Moviedb.Models;
using Moviedb.Repository;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Moviedb.Controllers
{
    public class MoviesController : Controller
    {
        // ReSharper disable once InconsistentNaming
        private readonly MovieRepository movieRepository;

        public MoviesController()
        {
            movieRepository = new MovieRepository();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var t = movieRepository.GetMovies().ToList();
            return View(t);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = movieRepository.GetMovie(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.ProducerId = new SelectList(movieRepository.GetProducers(), "Id", "Name");
            ViewBag.Actors = movieRepository.GetActors().ToList();
            return View("Form");
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ReleaseDate,Plot,ProducerId")] Movie movie)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    String[] actorIds = Request["Actors"].Split(',');
                    movie.MoviePosterPath = SaveFile(Request.Files[0]);

                    for (int i = 0; i < actorIds.Length; i++)
                        movie.Actors.Add(movieRepository.GetActor(int.Parse(actorIds[i])));

                    movieRepository.AddMovie(movie);
                    movieRepository.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.ProducerId = new SelectList(movieRepository.GetProducers(), "Id", "Name");
                    ViewBag.Actors = movieRepository.GetActors();

                    return Create();
                }
            }

            ViewBag.ProducerId = new SelectList(movieRepository.GetProducers(), "Id", "Name", movie.ProducerId);
            ViewBag.Actors = movieRepository.GetActors();
            return View("Form");
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieRepository.GetMovie(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProducerId = new SelectList(movieRepository.GetProducers(), "Id", "Name", movie.ProducerId);
            ViewBag.NewProducer = new Producer();
            ViewBag.NewActor = new Actor();
            ViewBag.Actors = movieRepository.GetActors();
            return View("Form", movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ReleaseDate,Plot,ProducerId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                String[] str;
                try
                {
                    str = Request["Actors"].Split(',');
                }
                catch (Exception)
                {
                    return Edit(movie.Id);
                }

                var movieDb = movieRepository.GetMovie(movie.Id);
                for (int i = 0; i < str.Length; i++)
                {
                    try
                    {
                        movieDb.Actors.Add(movieRepository.GetActor(int.Parse(str[i])));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(@"Exception {0}", e);
                    }
                }

                movieDb.Name = movie.Name;
                movieDb.Plot = movie.Plot;
                movieDb.ReleaseDate = movie.ReleaseDate;
                movieDb.ProducerId = movie.ProducerId;
                //movieDb.Actors = movie.Actors;

                movieRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(movieRepository.GetProducers(), "Id", "Name", movie.ProducerId);

            return View("Form", movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieRepository.GetMovie(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            Movie movie = movieRepository.GetMovie(id);
            movieRepository.RemoveMovie(movie);
            movieRepository.SaveChanges();

            if (movie.MoviePosterPath != null)
            {
                var filePath = "D:\\Images\\MoviePosters\\" + movie.MoviePosterPath.Split('/').Last();
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            return Json(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = movieRepository.GetMovie(id);
            movieRepository.RemoveMovie(movie ?? throw new InvalidOperationException());
            movieRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        private string SaveFile(HttpPostedFileBase file)
        {
            if (!String.IsNullOrEmpty(file.FileName))
            {
                var path = "D:\\Images\\MoviePosters\\";
                var myfileName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{file.FileName}";
                var myFilePath = path + myfileName;
                var filePathWebPage = "http://moviedb.kishan.com/moviePosters/" + myfileName;
                file.SaveAs(myFilePath);
                return filePathWebPage;
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                movieRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}