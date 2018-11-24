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
    public class MGEmployeesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: MGEmployees
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            IPrepareExtendedView<List<EmployeeExtended>, string> prepareStartSurveyView = new PrepareManagerEmployeeView<List<EmployeeExtended>, string>();
            prepareStartSurveyView.Parameters = userId;
            List<EmployeeExtended> employees = prepareStartSurveyView.GetView(_db);

            return View(employees);
        }

        // GET: MGEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.T_Employees.Find(id);

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
            return View(modelExtendedLoader.GetView(_db));
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
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Surveys/Edit/5
        public ActionResult EditSurvey(int? id)
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

            List<Employee> employees = _db.T_Employees.Where(i => i.Id == survey.EmployeeId).ToList();
            var userId = employees[0].UserId;
            var parameters = new { UserId = userId, Survey = survey };

            IViewBagExtendedLoader<dynamic> prepareSurveyViewBagLoader = new PrepareSurveyViewBagLoader<dynamic>();
            prepareSurveyViewBagLoader.Parameters = parameters;
            prepareSurveyViewBagLoader.Load(this, _db);

            return View(survey);
        }

        // GET: Surveys/ViewSurvey/5
        public ActionResult ViewSurvey(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IPrepareExtendedView<BrowseSurvey, int?> prepareStartSurveyView = new PrepareSurveyBrowseView<BrowseSurvey, int?>();
            prepareStartSurveyView.Parameters = id;
            BrowseSurvey browseSurvey = prepareStartSurveyView.GetView(_db);
            
            List<Employee> employees = _db.T_Employees.Where(i => i.Id == browseSurvey.Survey.EmployeeId).ToList();
            ViewBag.UserInfo = employees[0].FirstName + " " + employees[0].LastName;

            if (browseSurvey == null)
            {
                return HttpNotFound();
            }
            return View(browseSurvey);
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
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeMGSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareStartSurveyView = new PrepareNextSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareStartSurveyView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareStartSurveyView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult PrevSection(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PreparePrevMGSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);

        }
        [HttpPost]
        public JsonResult NextSectionQuestion(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionMGQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult FirstQuestion(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionMGFirstQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult NextQuestion(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeMGSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionMGQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult PrevQuestion(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeMGSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionPrevMGQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult FirstSectionQuestion(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionPrevMGQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult SectionHeader(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeMGSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareStartSurveyView = new PrepareSectionHeaderView<SurveyUserDataReturn, SurveyUserData>();
            prepareStartSurveyView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareStartSurveyView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult CompleteSurveyn(SurveyUserData model)
        {
            ISaveModel<SurveyUserData> saveEmployeeSurveyScore = new SaveEmployeeMGSurveyScore<SurveyUserData>();
            saveEmployeeSurveyScore.Save(model, _db);

            ISaveModel<SurveyUserData> saveEmployeeSurveyComplete = new SaveEmployeeMGSurveyComplete<SurveyUserData>();
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
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PreparePrevMGSectionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);

        }
        [HttpPost]
        public JsonResult NextSectionQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionMGQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult FirstQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionMGFirstQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult NextQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionMGQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult PrevQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionPrevMGQuestionView<SurveyUserDataReturn, SurveyUserData>();
            prepareExtendedView.Parameters = model;
            SurveyUserDataReturn surveyUserDataReturn = prepareExtendedView.GetView(_db);
            return getResult(surveyUserDataReturn);
        }
        [HttpPost]
        public JsonResult FirstSectionQuestionView(SurveyUserData model)
        {
            IPrepareExtendedView<SurveyUserDataReturn, SurveyUserData> prepareExtendedView = new PrepareSectionPrevMGQuestionView<SurveyUserDataReturn, SurveyUserData>();
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
