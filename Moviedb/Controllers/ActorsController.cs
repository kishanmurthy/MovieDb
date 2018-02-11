using Moviedb.Models;
using System.Net;
using System.Web.Mvc;
using Moviedb.Repository;

namespace Moviedb.Controllers
{
    public class ActorsController : Controller
    {

        
        private readonly ActorRepository _actorRepository;

        public ActorsController()
        {
            _actorRepository = new ActorRepository();
        }
        
        // GET: Actors
        public ActionResult Index()
        {
            return View(_actorRepository.GetActors());
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Actor actor = _actorRepository.GetActor(id);

            return actor == null ? HttpNotFound() : (ActionResult)View(actor);
        }

        // GET: Actors/Create
        public ActionResult Create()
        {
            return View("CreateEdit");
        }


        [HttpPost]
        public ActionResult CreateActor([Bind(Include = "Id,Name,Gender,DOB,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _actorRepository.AddActor(actor);
                _actorRepository.SaveChanges();   

                return Json(actor);
            }

            return Json(null);
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Gender,DOB,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _actorRepository.AddActor(actor);
                _actorRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreateEdit",actor);
        }

        // GET: Actors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            


            Actor actor = _actorRepository.GetActor(id);
            return actor == null ? HttpNotFound() : (ActionResult)View("CreateEdit",actor);
 
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,DOB,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _actorRepository.UpdateActor(actor);
                _actorRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("CreateEdit",actor);
        }
        
        public ActionResult Delete(int id)
        {
            Actor actor = _actorRepository.GetActor(id);
            _actorRepository.RemoveActor(actor);
            _actorRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _actorRepository.Dispose();
            
            base.Dispose(disposing);
        }
    }
}