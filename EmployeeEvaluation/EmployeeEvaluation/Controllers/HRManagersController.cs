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
    [Authorize(Roles = "HR Manager")]
    public class HRManagersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HRManagers
        public ActionResult Index()
        {
            IPrepareView<List<EmployeeExtended>> prepareView = new PrepareManagerView<List<EmployeeExtended>>();
            return View(prepareView.GetView(db));
        }

        // GET: HRManagers/Details/5
        public ActionResult Details(int? id)
        {
            IPrepareExtendedView<ManagerStructure, int?> modelExtendedLoader = new PrepareManagerDeatilsView<ManagerStructure, int?>();
            modelExtendedLoader.Parameters = id;
            return View(modelExtendedLoader.GetView(db));
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
