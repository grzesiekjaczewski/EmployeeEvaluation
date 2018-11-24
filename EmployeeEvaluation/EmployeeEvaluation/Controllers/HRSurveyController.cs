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

namespace EmployeeEvaluation.Controllers
{
    public class HRSurveyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HRSurvey
        public ActionResult Index()
        {
            IPrepareView<List<HRBrowseSurvey>> prepare = new PrepareHRSurveyView<List<HRBrowseSurvey>>();
            List<HRBrowseSurvey> view = prepare.GetView(db);

            return View(view);
        }

        // GET: HRSurvey/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.T_Survey.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
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
