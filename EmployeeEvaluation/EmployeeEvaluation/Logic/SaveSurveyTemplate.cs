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
            SurveyTemplateExtended surveyTemplateExtended = model as SurveyTemplateExtended;

            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(surveyTemplateExtended.Id);
            if (surveyTemplate != null)
            {
                surveyTemplate.Name = surveyTemplateExtended.Name;
                surveyTemplate.SurveyDate = surveyTemplateExtended.SurveyDate;

                db.Entry(surveyTemplate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}