﻿using System.Net;
using System.Web.Mvc;
using Moviedb.Models;
using Moviedb.Repository;
namespace Moviedb.Controllers
{
    public class ProducersController : Controller
    {
        

        private MovieRepository movieRepository;

        public ProducersController()
        {
            movieRepository = new MovieRepository();

        }
        // GET: Producers
        public ActionResult Index()
        {
            return View(movieRepository.GetAllProducers());
        }

        // GET: Producers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = movieRepository.FindProducer(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // GET: Producers/Create
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult CreateProducer()
        {

            return View();
        }



        [HttpPost]
        public ActionResult CreateProducer([Bind(Include = "Id,Name,Gender,DOB,Bio")]Producer producer)
        {
            if (ModelState.IsValid)
            {
                movieRepository.AddProducerToDb(producer);
                movieRepository.SaveChanges();

                
                return Json(producer);
            }

            return Json(null);
        }

        // POST: Producers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Gender,DOB,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                movieRepository.AddProducerToDb(producer);
                movieRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producer);
        }

        // GET: Producers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producer producer = movieRepository.FindProducer(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,DOB,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                movieRepository.UpdateProducer(producer);
                movieRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producer producer = movieRepository.FindProducer(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producer producer = movieRepository.FindProducer(id);
            if (producer != null)
                movieRepository.RemoveProducer(producer);
            movieRepository.SaveChanges();
            return RedirectToAction("Index");
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
