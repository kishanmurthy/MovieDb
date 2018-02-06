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
        private MyDBContext db = new MyDBContext();

        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Producer).Include(m => m.Actors);
            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name");
            ViewBag.Actors = db.Actors.ToList();
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ReleaseDate,Plot,ProducerId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String[] str = Request["Actors"].Split(',');

                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];

                        var fileName = file.FileName;
                        if (fileName != "")
                        {
                            var path = "D:\\Images\\MoviePosters\\";
                            var MyfileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + "_" + fileName;
                            var MyFilePath = path + MyfileName;
                            var filePathWebPage = "http://moviedb.kishan.com/moviePosters/" + MyfileName;
                            file.SaveAs(MyFilePath);
                            movie.MoviePosterPath = filePathWebPage;
                        }
                    }
                    for (int i = 0; i < str.Length; i++)
                        movie.Actors.Add(db.Actors.Find(int.Parse(str[i])));

                    db.Movies.Add(movie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name");
                    ViewBag.Actors = db.Actors.ToList();

                    return Create();
                }
            }

            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", movie.ProducerId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", movie.ProducerId);
            ViewBag.NewProducer = new Producer();
            ViewBag.NewActor = new Actor();
            ViewBag.Actors = db.Actors.ToList();
            return View(movie);
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

                var movieDb = db.Movies.Single(m => m.Id == movie.Id);
                for (int i = 0; i < str.Length; i++)
                {
                    try
                    {
                        movieDb.Actors.Add(db.Actors.Find(int.Parse(str[i])));
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine("Exception {0}", E);
                    }
                }

                movieDb.Name = movie.Name;
                movieDb.Plot = movie.Plot;
                movieDb.ReleaseDate = movie.ReleaseDate;
                movieDb.ProducerId = movie.ProducerId;
                //movieDb.Actors = movie.Actors;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", movie.ProducerId);

            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            
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
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetActors()
        {
            var actors = db.Actors.ToList();
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}