using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Logic.PrepareView;
using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeEvaluation.Controllers
{
    [Authorize]
    [Authorize(Roles = "HR Manager")]
    public class HRStructureController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HRStructure
        public ActionResult Structure()
        {

            IPrepareView<Structure>prepareView = new PrepareStructureView<Structure>();
            Structure structure = prepareView.GetView(db);

            return View(structure);
        }
    }
}