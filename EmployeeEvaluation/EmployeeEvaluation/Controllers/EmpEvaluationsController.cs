using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Models;
using Microsoft.AspNet.Identity;

namespace EmployeeEvaluation.Controllers
{
    public class EmpEvaluationsController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Surveys
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            List<Employee>employees = _db.T_Employees.Where(i => i.UserId == userId).ToList();

            int employeeId = 0;
            if (employees.Count > 0)
            {
                employeeId = employees[0].Id;
            }

            IPrepareExtendedView<List<SurveyDisplay>, int?> modelExtendedLoader = new PrepareSurveyView<List<SurveyDisplay>, int?>();
            modelExtendedLoader.Parameters = employeeId;
            return View(modelExtendedLoader.GetView(_db));
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = _db.T_Survey.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,ManagerId,SurveyTemplateId,SurveyStatusId,Name,SurveyDate,SurveyDadline,CompliteEmployeeDate,CompliteManagerDate,EmployeeSummary,EmployeeSummaryScore,ManagerSummary,ManagerSummaryScore,EmployeeCompleted,ManagerCompleted")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                _db.T_Survey.Add(survey);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(survey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = _db.T_Survey.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,ManagerId,SurveyTemplateId,SurveyStatusId,Name,SurveyDate,SurveyDadline,CompliteEmployeeDate,CompliteManagerDate,EmployeeSummary,EmployeeSummaryScore,ManagerSummary,ManagerSummaryScore,EmployeeCompleted,ManagerCompleted")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(survey).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = _db.T_Survey.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = _db.T_Survey.Find(id);
            _db.T_Survey.Remove(survey);
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
