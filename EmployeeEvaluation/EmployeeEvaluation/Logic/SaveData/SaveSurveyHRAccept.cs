using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.SaveData
{
    public class SaveSurveyHRAccept<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            var surveyUserData = model as Tuple<string, string>;
            Survey survey = db.T_Survey.Find(StringToValue.ParseInt(surveyUserData.Item1));
            survey.HRSummary = surveyUserData.Item2;
            survey.SurveyStatusId = 4;
            db.Entry(survey).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}