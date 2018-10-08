using EmployeeEvaluation.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PrepareManagerView<T> : IPrepareView<T> where T : class
    {
        public T GetView(ApplicationDbContext db)
        {
            var context = new IdentityDbContext();
            var users = context.Users.ToList();

            List<EmployeeExtended> employeeList =
               (from e in db.T_Employees
                join p in db.T_Positions on e.PositionId equals p.Id
                into Joinp
                from jp in Joinp.DefaultIfEmpty()
                join t in db.T_Teams.DefaultIfEmpty() on e.TeamId equals t.Id
                into Joint
                from jt in Joint.DefaultIfEmpty()
                select new EmployeeExtended
                {
                    Id = e.Id,
                    TeamName = jt.Name,
                    TeamId = e.TeamId,
                    PositionName = jp.Name,
                    PositionId = e.PositionId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserId = e.UserId
                }).ToList();

            foreach (EmployeeExtended ee in employeeList)
            {
                if (db.T_Teams.Where(t => t.ManagerId == ee.Id).Count() > 0)
                {
                    ee.IsManager = true;
                }
                else
                {
                    ee.IsManager = false;
                }
            }

            List<EmployeeExtended> employeeList2 =
                (from e in employeeList
                 join u in users on new { userId = e.UserId } equals new { userId = u.Id }
                 where e.IsManager == true
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
                     EMail = u.Email
                 }).ToList();

            return employeeList2 as T;
        }
    }
}