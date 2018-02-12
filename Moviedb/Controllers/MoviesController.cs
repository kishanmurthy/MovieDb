using Moviedb.DAL;
using Moviedb.Helper;
using Moviedb.Models;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Moviedb.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDal _movieDal;

        public MoviesController()
        {
            _movieDal = new MovieDal();
        }

        // GET: Movies
        public ActionResult Index()
        {
            return View(_movieDal.MovieRepository.GetMovies());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = _movieDal.MovieRepository.GetMovie(id);
            return movie == null ? (ActionResult)HttpNotFound() : View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.ProducerId = new SelectList(_movieDal.ProducerRepository.GetProducers(), "Id", "Name");
            ViewBag.Actors = _movieDal.ActorRepository.GetActors();
            return View("Create");
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
                    var actorsToAdd = _movieDal.ActorRepository.GetActors(actorIds.Select(e => Convert.ToInt32(e)).ToArray());
                    foreach (var actor in actorsToAdd)
                    {
                        movie.Actors.Add(actor);
                    }

                    _movieDal.MovieRepository.AddMovie(movie);

                    _movieDal.Save();

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return Content("Error Occured");
                }
            }

            ViewBag.ProducerId = new SelectList(_movieDal.ProducerRepository.GetProducers(), "Id", "Name", movie.ProducerId);
            ViewBag.Actors = _movieDal.ActorRepository.GetActors();
            return View("Create");
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = _movieDal.MovieRepository.GetMovie(id);
            if (movie == null)
                return HttpNotFound();

            ViewBag.ProducerId = new SelectList(_movieDal.ProducerRepository.GetProducers(), "Id", "Name", movie.ProducerId);
            ViewBag.NewProducer = new Producer();
            ViewBag.NewActor = new Actor();
            ViewBag.Actors = _movieDal.ActorRepository.GetActors();
            return View("Create", movie);
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
                String[] actorIds;
                try
                {
                    actorIds = Request["Actors"].Split(',');
                }
                catch (Exception)
                {
                    return Edit(movie.Id);
                }

                var movieDb = _movieDal.MovieRepository.GetMovie(movie.Id);
                var actorsToAdd = _movieDal.ActorRepository.GetActors(actorIds.Select(e => Convert.ToInt32(e)).ToArray());

                foreach (var actor in actorsToAdd)
                {
                    if (!movieDb.Actors.Contains(actor))
                        movieDb.Actors.Add(actor);
                }

                var actors = movieDb.Actors.ToArray();
                foreach (var actor in actors)
                {
                    if (!actorsToAdd.Contains(actor))
                        movieDb.Actors.Remove(actor);
                }

                movieDb.Name = movie.Name;
                movieDb.Plot = movie.Plot;
                movieDb.ReleaseDate = movie.ReleaseDate;
                movieDb.ProducerId = movie.ProducerId;

                _movieDal.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(_movieDal.ProducerRepository.GetProducers(), "Id", "Name", movie.ProducerId);

            return View("Create", movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = _movieDal.MovieRepository.GetMovie(id);

            return (movie == null) ? (ActionResult)HttpNotFound() : View(movie);
        }

        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            Movie movie = _movieDal.MovieRepository.GetMovie(id);
            _movieDal.MovieRepository.RemoveMovie(movie);
            _movieDal.MovieRepository.SaveChanges();

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
            Movie movie = _movieDal.MovieRepository.GetMovie(id);
            _movieDal.MovieRepository.RemoveMovie(movie ?? throw new InvalidOperationException());
            _movieDal.MovieRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        private string SaveFile(HttpPostedFileBase file)
        {
            if (!String.IsNullOrEmpty(file.FileName))
            {
                var path = MovieHelper.MoviePosterFileSystemPath;
                var myfileName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{file.FileName}";
                var myFilePath = path + myfileName;
                var filePathWebPage = MovieHelper.MoviePosterWebPath + myfileName;
                file.SaveAs(myFilePath);
                return filePathWebPage;
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _movieDal.MovieRepository.Dispose();

            base.Dispose(disposing);
        }
    }
}