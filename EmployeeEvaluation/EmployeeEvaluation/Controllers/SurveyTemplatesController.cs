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
    public class SurveyTemplatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SurveyTemplates
        public ActionResult Index()
        {
            return View(db.T_SurveyTemplate.ToList());
        }

        // GET: SurveyTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            if (surveyTemplate == null)
            {
                return HttpNotFound();
            }
            return View(surveyTemplate);
        }

        // GET: SurveyTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SurveyTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SurveyDate")] SurveyTemplate surveyTemplate)
        {
            var surveyDate = Request["SurveyDate1"];
            surveyTemplate.SurveyDate = Logic.CalculateDate.StringToDate(surveyDate, ".", "/", "-");
            //_controllerVieBagHelper.PrepareViewBagDictionaryForEdit(this, db);

            if (ModelState.IsValid)
            {
                db.T_SurveyTemplate.Add(surveyTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(surveyTemplate);
        }

        // GET: SurveyTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            if (surveyTemplate == null)
            {
                return HttpNotFound();
            }
            return View(surveyTemplate);
        }

        // POST: SurveyTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SurveyDate")] SurveyTemplate surveyTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(surveyTemplate);
        }

        // GET: SurveyTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            if (surveyTemplate == null)
            {
                return HttpNotFound();
            }
            return View(surveyTemplate);
        }

        // POST: SurveyTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            db.T_SurveyTemplate.Remove(surveyTemplate);
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
