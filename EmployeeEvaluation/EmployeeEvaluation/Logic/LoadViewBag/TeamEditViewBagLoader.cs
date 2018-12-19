using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeEvaluation.Logic
{
    public class TeamEditViewBagLoader<T> : IViewBagExtendedLoader<T>
    {
        public T Parameters { get; set; }

        public void Load(Controller controller, ApplicationDbContext db)
        {
            List<string> managers = Parameters as List<string>;
            var mgrs =
           (from e in db.T_Employees
            join m in managers on e.UserId equals m
            select new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.FirstName + " " + e.LastName
            }).ToList();

            controller.ViewBag.Managers = mgrs;
        }
    }
}