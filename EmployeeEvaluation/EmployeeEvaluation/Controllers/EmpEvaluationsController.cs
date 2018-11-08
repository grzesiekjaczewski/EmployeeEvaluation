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
using EmployeeEvaluation.Logic.SaveData;
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

            IPrepareExtendedView<BrowseSurvey, string> prepareStartSurveyView = new PrepareSurveyBrowseView<BrowseSurvey, string>();
            prepareStartSurveyView.Parameters = "";
            BrowseSurvey browseSurvey = prepareStartSurveyView.GetView(_db);


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
        public JsonResult StartSurvey(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareStartSurveyView = new PrepareSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareStartSurveyView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareStartSurveyView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult NextSection(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareStartSurveyView = new PrepareNextSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareStartSurveyView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareStartSurveyView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult PrevSection(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PreparePrevSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);

        }
        [HttpPost]
        public JsonResult NextSectionQuestion(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult FirstQuestion(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionFirstQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult NextQuestion(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult PrevQuestion(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionPrevQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult FirstSectionQuestion(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionPrevQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult SectionHeader(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareStartSurveyView = new PrepareSectionHeaderView<SurveyUserDataReturn, SurveyUserData>();
            prepareStartSurveyView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareStartSurveyView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult CompleteSurveyn(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            ISaveModel<SurveyUserData> saveEmployeeSurveyComplete = new SaveEmployeeSurveyComplete<SurveyUserData>();
            saveEmployeeSurveyComplete.Save(model, _db);

            SurveyUserDataReturn surveyUserDataReturn = new SurveyUserDataReturn();
            return getResult(surveyUserDataReturn);
        }


        [HttpPost]
        public JsonResult StartSurveyView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareStartSurveyView = new PrepareSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareStartSurveyView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareStartSurveyView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult NextSectionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareStartSurveyView = new PrepareNextSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareStartSurveyView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareStartSurveyView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult PrevSectionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PreparePrevSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);

        }
        [HttpPost]
        public JsonResult NextSectionQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult FirstQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionFirstQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult NextQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult PrevQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionPrevQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult FirstSectionQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionPrevQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult SectionHeaderView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareStartSurveyView = new PrepareSectionHeaderView<SurveyUserDataReturn, SurveyUserData>();
            prepareStartSurveyView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareStartSurveyView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }


        private JsonResult getResult(SurveyUserDataReturn surveyUserDataReturn)
        {
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
