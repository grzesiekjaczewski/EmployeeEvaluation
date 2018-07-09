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
    [Authorize(Roles = "Pracownik")]
    public class HREmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HREmployees
        public ActionResult Index()
        {
            IPrepareView <List<EmployeeExtended>> prepareView = new PrepareEmployeeView<List<EmployeeExtended>>();
            return View(prepareView.GetView(db));
        }

        // GET: HREmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.T_Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: HREmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.T_Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            IViewBagLoader viewBagLoader = new EmployeeEditViewBagLoader();
            viewBagLoader.Load(this, db);

            return View(employee);
        }

        // POST: HREmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,IsManager,TeamId,PositionId,FirstName,LastName")] Employee employee)
        {
            IViewBagLoader viewBagLoader = new EmployeeEditViewBagLoader();
            viewBagLoader.Load(this, db);

            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: HREmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.T_Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: HREmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.T_Employees.Find(id);
            db.T_Employees.Remove(employee);
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
