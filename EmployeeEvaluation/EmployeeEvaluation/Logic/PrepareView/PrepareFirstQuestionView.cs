using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PrepareFirstQuestionView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            SurveyUserData model = Parameters as SurveyUserData;
            SurveyUserDataReturn surveyUserDataReturn = new SurveyUserDataReturn();

            SurveyPart surveyPart = new SurveyPart();
            SurveyQuestion surveyQuestion = new SurveyQuestion();

            SurveyPartTemplate surveyPartTemplate = db.T_SurveyPartTemplate.Find(surveyPart.SurveyPartTemplateId);
            SurveyQuestionTemplate surveyQuestionTemplate = db.T_SurveyQuestionTemplate.Find(surveyQuestion.SurveyQuestionTemplateId);

            int id = StringToValue.ParseInt(model.Id);

            surveyPart = db.T_SurveyPart.Where(sp => sp.SurveyId == id).FirstOrDefault();
            surveyQuestion = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id).FirstOrDefault();

            surveyUserDataReturn.TotalSections = db.T_SurveyPart.Where(sp => sp.SurveyId == id).Count();
            surveyUserDataReturn.TotalQuestions = db.T_SurveyPart
                .Join(db.T_SurveyQuestion,
                sp => sp.Id, sq => sq.SurveyPartId,
                (sp, sq) => sp).Where(sp => sp.SurveyId == id).Count();
            surveyUserDataReturn.TotalSectionQuestions = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id).Count();



            return surveyUserDataReturn as T1;
        }
    }
}