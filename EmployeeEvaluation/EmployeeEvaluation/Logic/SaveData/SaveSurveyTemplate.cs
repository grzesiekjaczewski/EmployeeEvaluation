using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Logic
{

    public class SaveSurveyTemplate<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyTemplate surveyTemplate = model as SurveyTemplate;

            if (surveyTemplate != null)
            {
                if (surveyTemplate.PublishDate.Year < 1900) { surveyTemplate.PublishDate = new DateTime(1900, 1, 1); }
                db.Entry(surveyTemplate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}