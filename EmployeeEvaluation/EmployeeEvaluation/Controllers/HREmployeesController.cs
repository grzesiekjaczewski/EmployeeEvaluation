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
using EmployeeEvaluation.Logic.Remove;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EmployeeEvaluation.Controllers
{
    [Authorize]
    [Authorize(Roles = "HR Manager")]
    public class HREmployeesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public HREmployeesController()
        {
        }

        public HREmployeesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: HREmployees
        public ActionResult Index()
        {
            IPrepareView <List<EmployeeExtended>> prepareView = new PrepareEmployeeView<List<EmployeeExtended>>();
            return View(prepareView.GetView(_db));
        }

        // GET: HREmployees/Details/5
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

        // GET: HREmployees/Edit/5
        public ActionResult Edit(int? id)
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

            IViewBagLoader viewBagLoader = new EmployeeEditViewBagLoader();
            viewBagLoader.Load(this, _db);

            return View(employee);
        }

        // POST: HREmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,TeamId,PositionId,FirstName,LastName")] Employee employee)
        {
            IViewBagLoader viewBagLoader = new EmployeeEditViewBagLoader();
            viewBagLoader.Load(this, _db);

            if (ModelState.IsValid)
            {
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: HREmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                IRemoveItem removeEmployee = new RemoveEmployee();
                string userId = removeEmployee.Save(id, _db);
                if (userId.Length > 0)
                {
                    ApplicationUser user = UserManager.FindById(userId);
                    UserManager.Delete(user);
                }
            }

            return RedirectToAction("Index");
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
