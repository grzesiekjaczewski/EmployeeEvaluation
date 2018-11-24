using EmployeeEvaluation.Models;
using System.Data.Entity;

namespace EmployeeEvaluation.Logic.SaveData
{
    public class SaveEmployeeMGSurveyScore<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyUserData surveyUserData = model as SurveyUserData;

            if (surveyUserData != null && surveyUserData.QuestionId != null && surveyUserData.QuestionId != "0")
            {
                SurveyQuestion surveyQuestion = db.T_SurveyQuestion.Find(StringToValue.ParseInt(surveyUserData.QuestionId));
                surveyQuestion.ManagerScore = StringToValue.ParseInt(surveyUserData.QuestionSelection);
                if (surveyUserData.QuestionEmployeeComment != null && surveyUserData.QuestionEmployeeComment != "")
                {
                    surveyQuestion.ManagerComment = surveyUserData.QuestionEmployeeComment;
                }
                db.Entry(surveyQuestion).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}