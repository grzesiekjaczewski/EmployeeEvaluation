using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class SaveSurveyTemplateQuestionAdd<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyQuestionData modelData = model as SurveyQuestionData;

            int id, type;
            int.TryParse(modelData.Id, out id);
            int.TryParse(modelData.Type, out type);

            SurveyPartTemplate surveyPartTemplate = db.T_SurveyPartTemplate.Find(id);
            if (surveyPartTemplate != null)
            {
                SurveyQuestionTemplate surveyQuestionTemplate = new SurveyQuestionTemplate()
                {
                    Name = modelData.Name,
                    Definition = modelData.Criteria,
                    QuestionType = type,
                    SurveyPartTemplateId = id
                };
                db.T_SurveyQuestionTemplate.Add(surveyQuestionTemplate);
                db.Entry(surveyPartTemplate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}