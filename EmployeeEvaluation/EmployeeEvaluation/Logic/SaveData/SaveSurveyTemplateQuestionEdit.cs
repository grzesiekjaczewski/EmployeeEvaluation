using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class SaveSurveyTemplateQuestionEdit<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyQuestionData modelData = model as SurveyQuestionData;

            int id, type;
            int.TryParse(modelData.Id, out id);
            int.TryParse(modelData.Type, out type);

            SurveyQuestionTemplate surveyQuestionTemplate = db.T_SurveyQuestionTemplate.Find(id);
            if (surveyQuestionTemplate != null)
            {
                surveyQuestionTemplate.Name = modelData.Name;
                surveyQuestionTemplate.Definition = modelData.Criteria;
                surveyQuestionTemplate.QuestionType = type;
     
                db.Entry(surveyQuestionTemplate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}