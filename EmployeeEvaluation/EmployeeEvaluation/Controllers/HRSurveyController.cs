using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Logic.PrepareView;
using EmployeeEvaluation.Logic.SaveData;
using EmployeeEvaluation.Models;
using EmployeeEvaluation.Logic.PDF;

namespace EmployeeEvaluation.Controllers
{
    [Authorize]
    [Authorize(Roles = "HR Manager")]
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

        // GET: HRSurvey/AcceptSurvey/5
        public ActionResult AcceptSurvey(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IPrepareExtendedView<BrowseSurvey, int?> prepareStartSurveyView = new PrepareSurveyBrowseView<BrowseSurvey, int?>();
            prepareStartSurveyView.Parameters = id;
            BrowseSurvey browseSurvey = prepareStartSurveyView.GetView(db);

            List<Employee> employees = db.T_Employees.Where(i => i.Id == browseSurvey.Survey.EmployeeId).ToList();
            ViewBag.UserInfo = employees[0].FirstName + " " + employees[0].LastName;

            if (browseSurvey == null)
            {
                return HttpNotFound();
            }
            return View(browseSurvey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptSurvey()
        {
            var hrManagerComment = Request["hrManagerComment"];
            var id = Request["Survey.Id"];
            if (ModelState.IsValid)
            {
                Tuple<string, string> tuple = new Tuple<string, string>(id, hrManagerComment);
                ISaveModel<Tuple<string, string>> saveSurvey = new SaveSurveyHRAccept<Tuple<string, string>>();
                saveSurvey.Save(tuple, db);

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: HRSurvey/ViewSurvey/5
        public ActionResult ViewSurvey(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IPrepareExtendedView<BrowseSurvey, int?> prepareStartSurveyView = new PrepareSurveyBrowseView<BrowseSurvey, int?>();
            prepareStartSurveyView.Parameters = id;
            BrowseSurvey browseSurvey = prepareStartSurveyView.GetView(db);

            List<Employee> employees = db.T_Employees.Where(i => i.Id == browseSurvey.Survey.EmployeeId).ToList();
            ViewBag.UserInfo = employees[0].FirstName + " " + employees[0].LastName;

            if (browseSurvey == null)
            {
                return HttpNotFound();
            }
            return View(browseSurvey);
        }

        [HttpGet]
        public ActionResult GeneratePdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IPrepareExtendedView<BrowseSurvey, int?> prepareSurveyBrowseView = new PrepareSurveyBrowseView<BrowseSurvey, int?>();
            prepareSurveyBrowseView.Parameters = id;
            BrowseSurvey browseSurvey = prepareSurveyBrowseView.GetView(db);

            List<Employee> employees = db.T_Employees.Where(i => i.Id == browseSurvey.Survey.EmployeeId).ToList();
            int teamId = employees[0].TeamId;
            int positionId = employees[0].PositionId;
            Team team = db.T_Teams.Where(t => t.Id == teamId).FirstOrDefault();
            Position position = db.T_Positions.Where(p => p.Id == positionId).FirstOrDefault();


            PdfUtil objPdf = new PdfUtil();
            byte[] byteArray = objPdf.CreatePdf(browseSurvey, employees[0], team, position);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=Survey_" + 
                employees[0].FirstName + 
                "_" + 
                employees[0].LastName + 
                "_" +
                DateTime.Now.ToString() +
                ".pdf");

            Response.AddHeader("Content-Length", byteArray.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(byteArray);

            return View();
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
