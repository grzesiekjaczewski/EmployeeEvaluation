using EmployeeEvaluation.Models;
using Microsoft.AspNet.Identity.EntityFramework;
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
            var context = new IdentityDbContext();
            var users = context.Users.ToList();

            List<EmployeeExtended> employeeList = 
               (from e in db.T_Employees
                join p in db.T_Positions on e.PositionId equals p.Id 
                into Joinp from jp in Joinp.DefaultIfEmpty()
                join t in db.T_Teams.DefaultIfEmpty() on e.TeamId equals t.Id
                into Joint from jt in Joint.DefaultIfEmpty()
                join m in db.T_Employees.DefaultIfEmpty() on jt.ManagerId equals m.Id
                into Joinm from mg in Joinm.DefaultIfEmpty()

                select new EmployeeExtended
                {
                    Id = e.Id,
                    TeamName = jt.Name,
                    TeamId = e.TeamId,
                    PositionName = jp.Name,
                    PositionId = e.PositionId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserId = e.UserId,
                    Manager = mg.FirstName + " " + mg.LastName
                }).ToList();


            List<EmployeeExtended> employeeList2 =
                (from e in employeeList
                 join u in users on new { userId = e.UserId } equals new { userId = u.Id }
                 orderby e.FirstName + e.LastName
                 select new EmployeeExtended
                 {
                     Id = e.Id,
                     TeamName = e.TeamName,
                     TeamId = e.TeamId,
                     PositionName = e.PositionName,
                     PositionId = e.PositionId,
                     FirstName = e.FirstName,
                     LastName = e.LastName,
                     UserId = e.UserId,
                     EMail = u.Email,
                     Manager = e.Manager
                 }).ToList();

            return employeeList2 as T;
        }
    }
}