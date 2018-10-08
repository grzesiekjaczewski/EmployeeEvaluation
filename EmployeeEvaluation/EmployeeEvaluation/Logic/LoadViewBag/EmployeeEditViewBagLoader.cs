using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Logic
{
    public class EmployeeEditViewBagLoader : IViewBagLoader
    {
        public void Load(Controller controller, ApplicationDbContext db)
        {
            controller.ViewBag.Positions = db.T_Positions.OrderBy(n => n.Name).Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });

            controller.ViewBag.Teams = db.T_Teams.OrderBy(n => n.Name).Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            });

        }
    }
}