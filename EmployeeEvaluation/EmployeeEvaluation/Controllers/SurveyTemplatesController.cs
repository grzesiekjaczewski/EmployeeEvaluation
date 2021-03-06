﻿using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EmployeeEvaluation.Models;
using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Logic.CheckData;

namespace EmployeeEvaluation.Controllers
{
    [Authorize]
    [Authorize(Roles = "HR Manager")]
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
            surveyTemplate.SurveyDate = CalculateDate.StringToDate(surveyDate, ".", "/", "-");

            if (ModelState.IsValid)
            {
                if (surveyTemplate.PublishDate.Year < 1900) { surveyTemplate.PublishDate = new DateTime(1900, 1, 1); }
                db.T_SurveyTemplate.Add(surveyTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(surveyTemplate);
        }

        // GET: SurveyTemplates/View/5
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IPrepareExtendedView<SurveyTemplate, int?> modelExtendedLoader = new PrepareServeyDeatilsView<SurveyTemplate, int?>();
            modelExtendedLoader.Parameters = id;
            return View(modelExtendedLoader.GetView(db));
        }

        // GET: SurveyTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IPrepareExtendedView<SurveyTemplate, int?> modelExtendedLoader = new PrepareServeyDeatilsView<SurveyTemplate, int?>();
            modelExtendedLoader.Parameters = id;
            return View(modelExtendedLoader.GetView(db));
        }

        // POST: SurveyTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SurveyDate,NewPart,SummaryTitle")] SurveyTemplate surveyTemplate)
        {
            if (ModelState.IsValid)
            {
                var surveyDate = Request["SurveyDate1"];
                surveyTemplate.SurveyDate = CalculateDate.StringToDate(surveyDate, ".", "/", "-");

                ISaveModel<SurveyTemplate> saveSurveyTemplate = new SaveSurveyTemplate<SurveyTemplate>();
                saveSurveyTemplate.Save(surveyTemplate, db);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "HR Manager")]
        public ActionResult DeleteSurvey(int? id)
        {
                SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
                db.T_SurveyTemplate.Remove(surveyTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        [Authorize(Roles = "HR Manager")]
        [HttpPost]
        public JsonResult AddPart(SurveyPartData model)
        {
            int id;
            if (!int.TryParse(model.Id, out id))
            {
                return Json(new
                {
                    result = "Error"
                });
            }

            ISaveModel<SurveyPartData> saveSurveyTemplate = new SaveSurveyTemplatePartAdd<SurveyPartData>();
            saveSurveyTemplate.Save(model, db);

            return Json(new
            {
                result = "OK"
            });
        }


        [Authorize(Roles = "HR Manager")]
        [HttpPost]
        public JsonResult EditPart(SurveyPartData model)
        {
            int id;
            if (!int.TryParse(model.Id, out id))
            {
                return Json(new
                {
                    result = "Error"
                });
            }

            ISaveModel<SurveyPartData> saveSurveyTemplate = new SaveSurveyTemplatePartEdit<SurveyPartData>();
            saveSurveyTemplate.Save(model, db);

            return Json(new
            {
                result = "OK"
            });
        }

        [Authorize(Roles = "HR Manager")]
        [HttpPost]
        public JsonResult AddQuestion(SurveyQuestionData model)
        {
            int id;
            if (!int.TryParse(model.Id, out id))
            {
                return Json(new
                {
                    result = "Error"
                });
            }

            ISaveModel<SurveyQuestionData> saveSurveyTemplateQuestionAdd = new SaveSurveyTemplateQuestionAdd<SurveyQuestionData>();
            saveSurveyTemplateQuestionAdd.Save(model, db);

            return Json(new
            {
                result = "OK"
            });
        }

        [Authorize(Roles = "HR Manager")]
        [HttpPost]
        public JsonResult EditQuestion(SurveyQuestionData model)
        {
            int id;
            if (!int.TryParse(model.Id, out id))
            {
                return Json(new
                {
                    result = "Error"
                });
            }

            ISaveModel<SurveyQuestionData> saveSurveyTemplateQuestionEdit = new SaveSurveyTemplateQuestionEdit<SurveyQuestionData>();
            saveSurveyTemplateQuestionEdit.Save(model, db);

            return Json(new
            {
                result = "OK"
            });
        }

        [Authorize(Roles = "HR Manager")]
        public ActionResult DeletePart(int? id)
        {
            SurveyPartTemplate surveyPartTemplate = db.T_SurveyPartTemplate.Find(id);
            if (surveyPartTemplate != null)
            {
                db.T_SurveyPartTemplate.Remove(surveyPartTemplate);
                db.SaveChanges();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Edit/" + surveyPartTemplate.SurveyTemplateId.ToString());
        }

        [Authorize(Roles = "HR Manager")]
        public ActionResult DeleteQuestion(int? id)
        {
            SurveyQuestionTemplate surveyQuestionTemplate = db.T_SurveyQuestionTemplate.Find(id);
            SurveyPartTemplate surveyPartTemplate = db.T_SurveyPartTemplate.Find(surveyQuestionTemplate.SurveyPartTemplateId);

            //if (db.T_SurveyPartTemplate.Where(e => e.PositionId == id).ToList().Count() == 0)
            if (surveyQuestionTemplate != null)
            {
                db.T_SurveyQuestionTemplate.Remove(surveyQuestionTemplate);
                db.SaveChanges();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Edit/" + surveyPartTemplate.SurveyTemplateId.ToString());
        }

        [Authorize(Roles = "HR Manager")]
        [HttpPost]
        public JsonResult CopySurvey(SurveyPartData model)
        {
            string msg;
            ICheckData<SurveyPartData> checkSurveyTemplate = new CheckSurveyTemplate<SurveyPartData>();
            if (checkSurveyTemplate.Check(model, out msg, db) > 0)
            {
                return Json(new
                {
                    result = "Error",
                    message = msg
                });
            }

            ISaveModel<SurveyPartData> copySurvey = new CopySurvey<SurveyPartData>();
            copySurvey.Save(model, db);

            return Json(new
            {
                result = "OK"
            });
        }


        [Authorize(Roles = "HR Manager")]
        [HttpPost]
        public JsonResult PublishSurvey(SurveyPartData model)
        {
            string msg;
            ICheckData<SurveyPartData> checkSurveyTemplate = new CheckSurveyTemplate<SurveyPartData>();
            if (checkSurveyTemplate.Check(model, out msg, db) > 0)
            {
                return Json(new
                {
                    result = "Error",
                    message = msg
                });
            }

            ISaveModel<SurveyPartData> publishSurvey = new PublishSurvey<SurveyPartData>();
            publishSurvey.Save(model, db);

            return Json(new
            {
                result = "OK"
            });
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
