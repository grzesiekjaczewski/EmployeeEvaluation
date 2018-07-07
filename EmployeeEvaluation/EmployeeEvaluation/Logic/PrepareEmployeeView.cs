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
                from p in e.Positions.DefaultIfEmpty()
                from t in e.Teams.DefaultIfEmpty()
                select new EmployeeExtended
                {
                    Id = e.Id,
                    IsManager = e.IsManager,
                    TeamName = t.Name,
                    TeamId = e.TeamId,
                    PositionName = p.Name,
                    PositionId = e.PositionId,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }).ToList();

            return employeeList as T;
        }
    }
}