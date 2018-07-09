using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeEvaluation.Models;
using EmployeeEvaluation.Logic;

namespace EmployeeEvaluation.Controllers
{
    [Authorize]
    [Authorize(Roles = "HR Manager")]
    public class HRPositionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HRPositions
        public ActionResult Index()
        {
            IPrepareView<List<PositionExtended>> prepareView = new PreparePositionView<List<PositionExtended>>();
            return View(prepareView.GetView(db));
        }

        // GET: HRPositions/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: HRPositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.T_Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: HRPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(position);
        }

        // GET: HRPositions/Delete/5
        public ActionResult Delete(int? id)
        {
            Position position = db.T_Positions.Find(id);
            if (db.T_Employees.Where(e => e.PositionId == id).ToList().Count() == 0)
            {
                db.T_Positions.Remove(position);
                db.SaveChanges();
            }
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
