using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeEvaluation.Logic
{
    public class PrepareSurveyViewBagLoader<T> : IViewBagExtendedLoader<T> 
    {
        public T Parameters { get; set; }

        public void Load(Controller controller, ApplicationDbContext db)
        {
            dynamic parameters = Parameters as dynamic;
            string userId = parameters.UserId;
            Survey survey = parameters.Survey;
            db.Entry(survey).Collection(p => p.SurveyParts).Load();

            int spc = survey.SurveyParts.Count();
            int spq =
            (from sq in db.T_SurveyQuestion
             join sp in db.T_SurveyPart on sq.SurveyPartId equals sp.Id
             where sp.SurveyId == survey.Id
             select new { }
            ).ToList().Count();

            List <Employee> employees = db.T_Employees.Where(i => i.UserId == userId).ToList();
            if (employees.Count == 0) return;
            Employee employee = employees[0];

            controller.ViewBag.UserInfo = employee.FirstName + " " + employee.LastName;
            controller.ViewBag.SurveySections = spc;
            controller.ViewBag.SurveyQuestions = spq;
        }
    }
}