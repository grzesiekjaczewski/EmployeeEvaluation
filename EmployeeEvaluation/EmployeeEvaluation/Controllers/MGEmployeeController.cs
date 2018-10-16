using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Controllers
{
    public class MGEmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MGEmployee
        public ActionResult Index()
        {
            return View(db.EmployeeExtendeds.ToList());
        }

        // GET: MGEmployee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeExtended employeeExtended = db.EmployeeExtendeds.Find(id);
            if (employeeExtended == null)
            {
                return HttpNotFound();
            }
            return View(employeeExtended);
        }

        // GET: MGEmployee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MGEmployee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,TeamName,TeamId,PositionName,PositionId,FirstName,LastName,EMail,IsManager")] EmployeeExtended employeeExtended)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeExtendeds.Add(employeeExtended);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeExtended);
        }

        // GET: MGEmployee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeExtended employeeExtended = db.EmployeeExtendeds.Find(id);
            if (employeeExtended == null)
            {
                return HttpNotFound();
            }
            return View(employeeExtended);
        }

        // POST: MGEmployee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,TeamName,TeamId,PositionName,PositionId,FirstName,LastName,EMail,IsManager")] EmployeeExtended employeeExtended)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeExtended).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeExtended);
        }

        // GET: MGEmployee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeExtended employeeExtended = db.EmployeeExtendeds.Find(id);
            if (employeeExtended == null)
            {
                return HttpNotFound();
            }
            return View(employeeExtended);
        }

        // POST: MGEmployee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeExtended employeeExtended = db.EmployeeExtendeds.Find(id);
            db.EmployeeExtendeds.Remove(employeeExtended);
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
