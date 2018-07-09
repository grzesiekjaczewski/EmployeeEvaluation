using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PrepareTeamView<T> : IPrepareView<T> where T : class
    {
        public T GetView(ApplicationDbContext db)
        {
            List<TeamExtended> teamList =
               (from t in db.T_Teams
                join m in db.T_Employees on t.ManagerId equals m.Id
                into Joinm
                from jm in Joinm.DefaultIfEmpty()

                select new TeamExtended
                {
                    Id = t.Id,
                    Name = t.Name,
                    ManagerId = t.ManagerId,
                    ManagerName = jm.FirstName + " " + jm.LastName
                }).ToList();

            foreach(TeamExtended te in teamList)
            {
                if (db.T_Employees.Where(e => e.TeamId == te.Id).Count() > 0)
                {
                    te.Members = 1;
                }
                else
                {
                    te.Members = 0;
                }
            }

            return teamList as T;
        }
    }
}