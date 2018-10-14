using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Logic.SaveData
{
    public class SaveEmployeeSurveyScore<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyUserData surveyUserData = model as SurveyUserData;
            
            if (surveyUserData != null && surveyUserData.QuestionId != null && surveyUserData.QuestionId != "0")
            {
                SurveyQuestion surveyQuestion = db.T_SurveyQuestion.Find(StringToValue.ParseInt(surveyUserData.QuestionId));
                surveyQuestion.EmployeeScore = StringToValue.ParseInt(surveyUserData.QuestionSelection);
                if (surveyUserData.QuestionEmployeeComment != null && surveyUserData.QuestionEmployeeComment != "")
                { 
                    surveyQuestion.EmployeeComment = surveyUserData.QuestionEmployeeComment;
                }
                db.Entry(surveyQuestion).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}