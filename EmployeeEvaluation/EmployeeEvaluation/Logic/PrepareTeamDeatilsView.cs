using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Logic
{
    public class PrepareTeamDeatilsView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            int? id = Parameters as int?;
            if (id == null)
            {
                return null;
            }
            Team team = db.T_Teams.Find(id);
            if (team == null)
            {
                return null;
            }

            Employee manager = null;
            string managerName = "";

            manager = db.T_Employees.Find(team.ManagerId);
            if (manager != null)
            {
                managerName = manager.FirstName + " " + manager.LastName;
            }
            

            TeamStructure teamStructure = new TeamStructure();
            TeamExtended teamExtended = new TeamExtended()
            {
                ManagerName = managerName,
                Name = team.Name
            };

            List<EmployeeExtended>  TeamMembers = new List<EmployeeExtended>();

            teamStructure.TeamMembers =
           (from e in db.T_Employees
            join p in db.T_Positions on e.PositionId equals p.Id
            into Joinp
            from jp in Joinp.DefaultIfEmpty()
            where e.TeamId == id
            orderby e.FirstName + e.LastName
            select new EmployeeExtended
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                PositionName = jp.Name
            }).ToList();

            teamStructure.TeamExtended = teamExtended;
            
            return teamStructure as T1;
        }
    }
}