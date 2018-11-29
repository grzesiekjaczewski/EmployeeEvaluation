using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PrepareStructureView<T1> : IPrepareView<T1> where T1 : class
    {
        public T1 GetView(ApplicationDbContext db)
        {
            Structure structure = new Structure();
            structure.Boss = (from e in db.T_Employees
                              join t in db.T_Teams on e.TeamId equals t.Id
                              join p in db.T_Positions on e.PositionId equals p.Id
                              where t.ManagerId == e.Id
                              && t.Id != 1
                              select new Boss {
                                  FirstName = e.FirstName,
                                  LastName = e.LastName,
                                  Position = p.Name,
                                  Team = t.Name
                              }
                            ).ToList()[0];

            

            return structure as T1;
        }
    }
}