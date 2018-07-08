using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeEvaluation.Controllers
{
    [Authorize]
    [Authorize(Roles = "Pracownik")]
    public class MyProfileController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: MyProfile
        [Authorize]
        public ActionResult Index()
        {
            //IPrepareView<List<EmployeeExtended>> prepareView = new PrepareEmployeeUserView<List<EmployeeExtended>>();
            //return View(prepareView.GetView(_db));

            //var userId = User.Identity.GetUserId();

            //if (_db.T_Employees.Where(e => e.UserId == userId).Count() > 0)
            //{

            //}
            //else
            //{

            //}
                

            return View();
        }
    }
}