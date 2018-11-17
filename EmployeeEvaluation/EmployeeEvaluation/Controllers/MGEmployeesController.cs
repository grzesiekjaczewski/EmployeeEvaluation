using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Logic.PrepareView;
using EmployeeEvaluation.Models;
using Microsoft.AspNet.Identity;

namespace EmployeeEvaluation.Controllers
{
    public class MGEmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MGEmployees
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            IPrepareExtendedView<List<EmployeeExtended>, string> prepareStartSurveyView = new PrepareManagerEmployeeView<List<EmployeeExtended>, string>();
            prepareStartSurveyView.Parameters = userId;
            List<EmployeeExtended> employees = prepareStartSurveyView.GetView(db);

            return View(employees);
        }

        // GET: MGEmployees/Details/5
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


        // GET: MGEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IPrepareExtendedView<List<SurveyDisplay>, int?> modelExtendedLoader = new PrepareSurveyView<List<SurveyDisplay>, int?>();
            modelExtendedLoader.Parameters = id;
            return View(modelExtendedLoader.GetView(db));
        }

        // POST: MGEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,TeamId,PositionId,FirstName,LastName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
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
