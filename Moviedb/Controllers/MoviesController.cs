using Moviedb.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace Moviedb.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _db.Movies.Include(m => m.Producer).Include(m => m.Actors);
            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.ProducerId = new SelectList(_db.Producers, "Id", "Name");
            ViewBag.Actors = _db.Actors.ToList();
            return View("Form");
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ReleaseDate,Plot,ProducerId")] Movie movie)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    String[] str = Request["Actors"].Split(',');

                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];

                        // ReSharper disable once PossibleNullReferenceException
                        var fileName = file.FileName;
                        if (fileName != "")
                        {
                            var path = "D:\\Images\\MoviePosters\\";
                            var myfileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + "_" + fileName;
                            var myFilePath = path + myfileName;
                            var filePathWebPage = "http://moviedb.kishan.com/moviePosters/" + myfileName;
                            file.SaveAs(myFilePath);
                            movie.MoviePosterPath = filePathWebPage;
                        }
                    }
                    for (int i = 0; i < str.Length; i++)
                        movie.Actors.Add(_db.Actors.Find(int.Parse(str[i])));

                    _db.Movies.Add(movie);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.ProducerId = new SelectList(_db.Producers, "Id", "Name");
                    ViewBag.Actors = _db.Actors.ToList();

                    return Create();
                }
            }

            ViewBag.ProducerId = new SelectList(_db.Producers, "Id", "Name", movie.ProducerId);
            ViewBag.Actors = _db.Actors.ToList();
            return View("Form");
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProducerId = new SelectList(_db.Producers, "Id", "Name", movie.ProducerId);
            ViewBag.NewProducer = new Producer();
            ViewBag.NewActor = new Actor();
            ViewBag.Actors = _db.Actors.ToList();
            return View("Form",movie);
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

                var movieDb = _db.Movies.Single(m => m.Id == movie.Id);
                for (int i = 0; i < str.Length; i++)
                {
                    try
                    {
                        movieDb.Actors.Add(_db.Actors.Find(int.Parse(str[i])));
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

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(_db.Producers, "Id", "Name", movie.ProducerId);

            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            Movie movie = _db.Movies.Find(id);
            _db.Movies.Remove(movie ?? throw new Exception());
            _db.SaveChanges();
            
            if (movie.MoviePosterPath != null)
            {

                var filePath = "D:\\Images\\MoviePosters\\" + movie.MoviePosterPath.Split('/').Last();
                if(System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            return Json(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = _db.Movies.Find(id);
            _db.Movies.Remove(movie ?? throw new InvalidOperationException());
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}