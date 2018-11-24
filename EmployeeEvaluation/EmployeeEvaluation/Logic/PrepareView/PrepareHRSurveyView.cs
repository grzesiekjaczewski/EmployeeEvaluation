using EmployeeEvaluation.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PrepareHRSurveyView<T> : IPrepareView<T> where T : class
    {
        public T GetView(ApplicationDbContext db)
        {
            var context = new IdentityDbContext();
            var users = context.Users.ToList();

            List<HRBrowseSurvey> surveys =
               (from e in db.T_Employees
                join p in db.T_Positions on e.PositionId equals p.Id
                into Joinp
                from jp in Joinp.DefaultIfEmpty()
                join s in db.T_Survey on e.Id equals s.EmployeeId
                into Joins
                from js in Joins.DefaultIfEmpty()
                join t in db.T_Teams.DefaultIfEmpty() on e.TeamId equals t.Id
                into Joint
                from jt in Joint.DefaultIfEmpty()
                where js.SurveyStatusId >= 3
                orderby js.SurveyStatusId, jt.Name, e.FirstName, e.LastName
                select new HRBrowseSurvey
                {
                    SurveyId = js.Id,
                    EmployeeId = e.Id,
                    Employee = e.FirstName + " " + e.LastName,
                    Team = jt.Name,
                    SurveyName = js.Name,
                    SurveyDate = js.SurveyDate,
                    SurveyDadline = js.SurveyDadline,
                    SurveyStatusId = js.SurveyStatusId
                }).ToList();
  
            return surveys as T;
        }
    }
}