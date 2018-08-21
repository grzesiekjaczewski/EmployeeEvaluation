using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class SaveSurveyTemplatePartEdit<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyPartData modelData = model as SurveyPartData;

            int id;
            int.TryParse(modelData.Id, out id);

            SurveyPartTemplate surveyPartTemplate = db.T_SurveyPartTemplate.Find(id);
            if (surveyPartTemplate != null)
            {
                surveyPartTemplate.Name = modelData.Name;
                surveyPartTemplate.SummaryTitle = modelData.Summary;

                db.Entry(surveyPartTemplate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}