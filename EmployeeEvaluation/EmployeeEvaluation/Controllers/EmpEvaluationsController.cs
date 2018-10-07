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
    [Authorize]
    [Authorize(Roles = "Pracownik")]
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

            var userId = User.Identity.GetUserId();
            var parameters = new { UserId = userId, Survey = survey };

            IViewBagExtendedLoader<dynamic> prepareSurveyViewBagLoader = new PrepareSurveyViewBagLoader<dynamic>();
            prepareSurveyViewBagLoader.Parameters = parameters;
            prepareSurveyViewBagLoader.Load(this, _db);

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


        [HttpPost]
        public JsonResult PrevSurveyPage(SurveyUserData model)
        {
            int id;
            if (!int.TryParse(model.Id, out id))
            {
                return Json(new
                {
                    result = "Error"
                });
            }
            //ISaveModel<SurveyPartData> saveSurveyTemplate = new PublishSurvey<SurveyPartData>();
            //saveSurveyTemplate.Save(model, db);

            return Json(new
            {
                success = true,
                result = "OK",
                JsonRequestBehavior.AllowGet
            });
        }


        [HttpPost]
        public JsonResult NextSurveyPage(SurveyUserData model)
        {
            int id;
            if (!int.TryParse(model.Id, out id))
            {
                return Json(new
                {
                    result = "Error"
                });
            }

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);

            return Json(new
            {
                success = true,
                result = "OK",
                surveyUserDataReturn = surveyUserDataReturn,
                JsonRequestBehavior.AllowGet
            });
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
