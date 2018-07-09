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
    public class HRTeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HRTeams
        public ActionResult Index()
        {
            IPrepareView<List<TeamExtended>> prepareView = new PrepareTeamView<List<TeamExtended>>();
            return View(prepareView.GetView(db));
        }

        // GET: HRTeams/Details/5
        public ActionResult Details(int? id)
        {
            IPrepareExtendedView<TeamStructure, int?> modelExtendedLoader = new PrepareTeamDeatilsView<TeamStructure, int?>();
            modelExtendedLoader.Parameters = id;
            return View(modelExtendedLoader.GetView(db));
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
                db.T_Teams.Add(team);
                db.SaveChanges();
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
            Team team = db.T_Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            IViewBagLoader viewBagLoader = new TeamEditViewBagLoader();
            viewBagLoader.Load(this, db);

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
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: HRTeams/Delete/5
        public ActionResult Delete(int? id)
        {
            Team team = db.T_Teams.Find(id);
            if (db.T_Employees.Where(e => e.TeamId == id).ToList().Count() == 0)
            {
                db.T_Teams.Remove(team);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
