using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class SaveSurveyTemplatePartAdd<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyPartData modelData = model as SurveyPartData;

            int id;
            int.TryParse(modelData.Id, out id);

            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            if (surveyTemplate != null)
            {
                SurveyPartTemplate surveyPartTemplate = new SurveyPartTemplate()
                {
                    Name = modelData.Name,
                    SummaryTitle = modelData.Summary,
                    SurveyTemplateId = id
                };

                db.T_SurveyPartTemplate.Add(surveyPartTemplate);
                db.Entry(surveyTemplate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}