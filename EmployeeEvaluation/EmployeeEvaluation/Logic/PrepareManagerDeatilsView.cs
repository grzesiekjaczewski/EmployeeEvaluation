using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PrepareManagerDeatilsView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            int? id = Parameters as int?;
            if (id == null)
            {
                return null;
            }

            Employee employee = db.T_Employees.Find(id);

            List<TeamExtended> teams = (from t in db.T_Teams
                                where t.ManagerId == id
                                select new TeamExtended
                                {
                                    Id = t.Id,
                                    Name = t.Name,
                                    ManagerId = t.ManagerId
                                }).ToList();

            foreach (TeamExtended t in teams)
            {
                t.MamberList = (from e in db.T_Employees
                                join p in db.T_Positions on e.PositionId equals p.Id
                                where e.TeamId == t.Id
                                select new EmployeeExtended
                                {
                                    Id = e.Id,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    PositionId = e.PositionId,
                                    PositionName = p.Name
                                }).ToList();
            }

            ManagerStructure managerStructure = new ManagerStructure()
            {
                ManagerName = employee.FirstName + " " + employee.LastName,
                Teams = teams
            };

            return managerStructure as T1;
        }
    }
}