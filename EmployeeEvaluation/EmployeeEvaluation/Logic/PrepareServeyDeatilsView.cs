using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PrepareServeyDeatilsView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            SurveyTemplateExtemded surveyTemplateExtemded = new SurveyTemplateExtemded()
            {
                //ManagerName = employee.FirstName + " " + employee.LastName,
                //Teams = teams
            };

            return surveyTemplateExtemded as T1;
        }
    }
}