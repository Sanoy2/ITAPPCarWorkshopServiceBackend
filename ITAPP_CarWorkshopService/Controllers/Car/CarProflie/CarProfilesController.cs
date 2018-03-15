using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITAPP_CarWorkshopService;

namespace ITAPP_CarWorkshopService.Controllers.Car.CarProflie
{
    public class CarProfilesController : Controller
    {
        private ITAPPCarWorkshopServiceDBEntities db = new ITAPPCarWorkshopServiceDBEntities();

        // GET: CarProfiles
        public ActionResult Index()
        {
            var car_Profiles = db.Car_Profiles.Include(c => c.Car_Brands);
            return View(car_Profiles.ToList());
        }

        // GET: CarProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Profiles car_Profiles = db.Car_Profiles.Find(id);
            if (car_Profiles == null)
            {
                return HttpNotFound();
            }
            return View(car_Profiles);
        }

        // GET: CarProfiles/Create
        public ActionResult Create()
        {
            ViewBag.Brand_ID = new SelectList(db.Car_Brands, "Brand_ID", "Brand_Name");
            return View();
        }

        // POST: CarProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Car_ID,Brand_ID,Car_model,Car_VIN_number,Car_production_year,Car_first_registration_year")] Car_Profiles car_Profiles)
        {
            if (ModelState.IsValid)
            {
                db.Car_Profiles.Add(car_Profiles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Brand_ID = new SelectList(db.Car_Brands, "Brand_ID", "Brand_Name", car_Profiles.Brand_ID);
            return View(car_Profiles);
        }

        // GET: CarProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Profiles car_Profiles = db.Car_Profiles.Find(id);
            if (car_Profiles == null)
            {
                return HttpNotFound();
            }
            ViewBag.Brand_ID = new SelectList(db.Car_Brands, "Brand_ID", "Brand_Name", car_Profiles.Brand_ID);
            return View(car_Profiles);
        }

        // POST: CarProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Car_ID,Brand_ID,Car_model,Car_VIN_number,Car_production_year,Car_first_registration_year")] Car_Profiles car_Profiles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car_Profiles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Brand_ID = new SelectList(db.Car_Brands, "Brand_ID", "Brand_Name", car_Profiles.Brand_ID);
            return View(car_Profiles);
        }

        // GET: CarProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Profiles car_Profiles = db.Car_Profiles.Find(id);
            if (car_Profiles == null)
            {
                return HttpNotFound();
            }
            return View(car_Profiles);
        }

        // POST: CarProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car_Profiles car_Profiles = db.Car_Profiles.Find(id);
            db.Car_Profiles.Remove(car_Profiles);
            db.SaveChanges();
            return RedirectToAction("Index");
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
