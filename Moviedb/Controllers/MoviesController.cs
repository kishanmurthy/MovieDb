using Moviedb.Models;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Moviedb.DAL;

namespace Moviedb.Controllers
{
    public class MoviesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public MoviesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Movies
        public ActionResult Index()
        {
            return View(_unitOfWork.MovieRepository.GetMovies());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = _unitOfWork.MovieRepository.GetMovie(id);
            return movie == null ? (ActionResult)HttpNotFound() : View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.ProducerId = new SelectList(_unitOfWork.ProducerRepository.GetProducers(), "Id", "Name");
            ViewBag.Actors = _unitOfWork.ActorRepository.GetActors();
            return View("CreateEdit");
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
                    var actorsToAdd = _unitOfWork.ActorRepository.GetActors(actorIds.Select(e => Convert.ToInt32(e)).ToArray());
                    foreach (var actor in actorsToAdd)
                    {
                        movie.Actors.Add(actor);
                    }

                    _unitOfWork.MovieRepository.AddMovie(movie);

                    
                    _unitOfWork.Save();
                    
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return Content("Error Occured");
                }
            }

            ViewBag.ProducerId = new SelectList(_unitOfWork.ProducerRepository.GetProducers(), "Id", "Name", movie.ProducerId);
            ViewBag.Actors = _unitOfWork.ActorRepository.GetActors();
            return View("CreateEdit");
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Movie movie = _unitOfWork.MovieRepository.GetMovie(id);
            if (movie == null)
                return HttpNotFound();

            ViewBag.ProducerId = new SelectList(_unitOfWork.ProducerRepository.GetProducers(), "Id", "Name", movie.ProducerId);
            ViewBag.NewProducer = new Producer();
            ViewBag.NewActor = new Actor();
            ViewBag.Actors = _unitOfWork.ActorRepository.GetActors();
            return View("CreateEdit", movie);
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

                var movieDb = _unitOfWork.MovieRepository.GetMovie(movie.Id);
                var actorsToAdd = _unitOfWork.ActorRepository.GetActors(actorIds.Select(e => Convert.ToInt32(e)).ToArray());

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

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(_unitOfWork.ProducerRepository.GetProducers(), "Id", "Name", movie.ProducerId);

            return View("CreateEdit", movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Movie movie = _unitOfWork.MovieRepository.GetMovie(id);

            return (movie == null) ? (ActionResult) HttpNotFound() : View(movie);
            
        }

        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            Movie movie = _unitOfWork.MovieRepository.GetMovie(id);
            _unitOfWork.MovieRepository.RemoveMovie(movie);
            _unitOfWork.MovieRepository.SaveChanges();

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
            Movie movie = _unitOfWork.MovieRepository.GetMovie(id);
            _unitOfWork.MovieRepository.RemoveMovie(movie ?? throw new InvalidOperationException());
            _unitOfWork.MovieRepository.SaveChanges();
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
                _unitOfWork.MovieRepository.Dispose();
            
            base.Dispose(disposing);
        }
    }
}