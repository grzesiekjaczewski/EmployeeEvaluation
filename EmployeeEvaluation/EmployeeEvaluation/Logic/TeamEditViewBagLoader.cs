using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeEvaluation.Logic
{
    //public class TeamEditViewBagLoader<T> : IViewBagExtendedLoader<T>
    public class TeamEditViewBagLoader : IViewBagLoader
    {
        //public T Parameters { get; set; }

        public void Load(Controller controller, ApplicationDbContext db)
        {
            controller.ViewBag.Managers = db.T_Employees
                .Where(e => e.IsManager == true)
                .OrderBy(n => n.FirstName + " " + n.LastName)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.FirstName + " " + p.LastName
                });

            //int? teamId = Parameters as int?;
            //controller.ViewBag.Managers = db.T_Employees
            //    .Where(e => e.IsManager == true && e.TeamId == teamId)
            //    .OrderBy(n => n.FirstName + " " + n.LastName)
            //    .Select(p => new SelectListItem
            //{
            //    Value = p.Id.ToString(),
            //    Text = p.FirstName + " " + p.LastName
            //});


        }
    }
}