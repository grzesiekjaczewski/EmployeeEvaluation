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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EmployeeEvaluation.Controllers
{
    [Authorize]
    [Authorize(Roles = "HR Manager")]
    public class HRTeamsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _db = new ApplicationDbContext();

        public HRTeamsController()
        {
        }

        public HRTeamsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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


        // GET: HRTeams
        public ActionResult Index()
        {
            IPrepareView<List<TeamExtended>> prepareView = new PrepareTeamView<List<TeamExtended>>();
            return View(prepareView.GetView(_db));
        }

        // GET: HRTeams/Details/5
        public ActionResult Details(int? id)
        {
            IPrepareExtendedView<TeamStructure, int?> modelExtendedLoader = new PrepareTeamDeatilsView<TeamStructure, int?>();
            modelExtendedLoader.Parameters = id;
            return View(modelExtendedLoader.GetView(_db));
        }

        // GET: HRTeams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ManagerId")] Team team)
        {
            if (ModelState.IsValid)
            {
                _db.T_Teams.Add(team);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: HRTeams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = _db.T_Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            List<string> managers = new List<string>();
            var mgrs = UserManager.Users.ToList();
            foreach (ApplicationUser au in mgrs)
            {
                if (UserManager.IsInRole(au.Id, "Manager"))
                {
                    managers.Add(au.Id);
                }
            }

            IViewBagExtendedLoader<List<string>> viewBagLoader = new TeamEditViewBagLoader<List<string>>();
            viewBagLoader.Parameters = managers;
            viewBagLoader.Load(this, _db);

            return View(team);
        }

        // POST: HRTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ManagerId")] Team team)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(team).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: HRTeams/Delete/5
        public ActionResult Delete(int? id)
        {
            Team team = _db.T_Teams.Find(id);
            if (_db.T_Employees.Where(e => e.TeamId == id).ToList().Count() == 0)
            {
                _db.T_Teams.Remove(team);
                _db.SaveChanges();
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
