using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.SaveData
{
    public class SaveEmployeeSurveyComplete<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyUserData surveyUserData = model as SurveyUserData;

            if (surveyUserData != null && surveyUserData.QuestionId != null && surveyUserData.QuestionId != "0")
            {
                Survey survey = db.T_Survey.Find(StringToValue.ParseInt(surveyUserData.Id));
                survey.CompliteEmployeeDate = DateTime.Now;
                survey.EmployeeCompleted = true;

                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}