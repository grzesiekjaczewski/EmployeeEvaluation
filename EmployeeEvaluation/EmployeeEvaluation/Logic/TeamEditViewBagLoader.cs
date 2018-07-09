using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeEvaluation.Logic
{
    public class TeamEditViewBagLoader : IViewBagLoader
    {
        public void Load(Controller controller, ApplicationDbContext db)
        {
            controller.ViewBag.Managers = db.T_Employees
                .OrderBy(n => n.FirstName + " " + n.LastName)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.FirstName + " " + p.LastName
                });
        }
    }
}