using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PreparePositionView<T> : IPrepareView<T> where T : class
    {
        public T GetView(ApplicationDbContext db)
        {
            List<PositionExtended> positionList =
           (from p in db.T_Positions
            select new PositionExtended
            {
                Id = p.Id,
                Name = p.Name,
                
            }).ToList();

            foreach (PositionExtended pe in positionList)
            {
                if (db.T_Employees.Where(e => e.PositionId == pe.Id).Count() > 0)
                {
                    pe.Members = 1;
                }
                else
                {
                    pe.Members = 0;
                }
            }

            return positionList as T;
        }
    }
}