using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PrepareEmployeeView<T> : IPrepareView<T> where T : class
    {
        public T GetView(ApplicationDbContext db)
        {
            List<EmployeeExtended> employeeList = 
               (from e in db.T_Employees
                join p in db.T_Positions on e.PositionId equals p.Id 
                into Joinp from jp in Joinp.DefaultIfEmpty()
                join t in db.T_Teams.DefaultIfEmpty() on e.TeamId equals t.Id
                into Joint from jt in Joint.DefaultIfEmpty()
                select new EmployeeExtended
                {
                    Id = e.Id,
                    TeamName = jt.Name,
                    TeamId = e.TeamId,
                    PositionName = jp.Name,
                    PositionId = e.PositionId,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }).ToList();

            return employeeList as T;
        }
    }
}