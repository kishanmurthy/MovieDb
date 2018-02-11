using System.Net;
using System.Web.Mvc;
using Moviedb.Models;
using Moviedb.Repository;
namespace Moviedb.Controllers
{
    public class ProducersController : Controller
    {
        

        private readonly ProducerRepository _producerRepository;

        public ProducersController()
        {
            _producerRepository = new ProducerRepository();

        }
        // GET: Producers
        public ActionResult Index()
        {
            return View(_producerRepository.GetProducers());
        }

        // GET: Producers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Producer producer = _producerRepository.GetProducer(id);
            return (producer == null) ? (ActionResult) HttpNotFound() : View(producer);
            
        }

        // GET: Producers/Create
        public ActionResult Create()
        {
            return View("CreateEdit");
        }



        [HttpPost]
        public ActionResult CreateProducer([Bind(Include = "Id,Name,Gender,DOB,Bio")]Producer producer)
        {
            if (ModelState.IsValid)
            {
                _producerRepository.AddProducer(producer);
                _producerRepository.SaveChanges();

                
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
                _producerRepository.AddProducer(producer);
                _producerRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreateEdit",producer);
        }

        // GET: Producers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Producer producer = _producerRepository.GetProducer(id);
            return (producer == null) ? (ActionResult)HttpNotFound() : View("CreateEdit",producer);
           
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
                _producerRepository.UpdateProducer(producer);
                _producerRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("CreateEdit",producer);
        }
 
       
        public ActionResult Delete(int id)
        {
            Producer producer = _producerRepository.GetProducer(id);
            if (producer != null)
                _producerRepository.RemoveProducer(producer);
            _producerRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _producerRepository.Dispose();
            
            base.Dispose(disposing);
        }
    }
}
