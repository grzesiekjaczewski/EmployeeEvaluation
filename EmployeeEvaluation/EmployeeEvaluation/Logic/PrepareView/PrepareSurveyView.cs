using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PrepareSurveyView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            int? employeeId = Parameters as int?;
            if (employeeId == null)
            {
                return null;
            }

            List<SurveyDisplay> surveyList =
               (from s in db.T_Survey
                join ss in db.T_SurveySatus on s.SurveyStatusId equals ss.Id
                where s.EmployeeId == employeeId
                orderby s.SurveyDadline descending

                select new SurveyDisplay
                {
                    Id = s.Id,
                    Name = s.Name,
                    Status = ss.Name,
                    SurveyDadline = s.SurveyDadline,
                    EmployeeCompleted = s.EmployeeCompleted,
                    ManagerCompleted = s.ManagerCompleted,
                    StatusId = s.SurveyStatusId
                }).ToList();

            return surveyList as T1;
        }
    }
}