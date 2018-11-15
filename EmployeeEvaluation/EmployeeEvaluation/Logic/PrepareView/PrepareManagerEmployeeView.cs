using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PrepareManagerEmployeeView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            string userId = Parameters as string;
            Employee manager = db.T_Employees.Where(m => m.UserId == userId).FirstOrDefault();
            Team team = db.T_Teams.Where(t => t.ManagerId == manager.Id).FirstOrDefault();
            
            List<EmployeeExtended> employees = new List<EmployeeExtended>();
            employees = db.T_Employees.Where(e => e.TeamId == team.Id).Select(e => new EmployeeExtended()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PositionName = db.T_Positions.Where(p => p.Id == e.PositionId).FirstOrDefault().Name,
                    TeamName = team.Name
                }
            ).ToList();

            return employees as T1;
        }
    }
}