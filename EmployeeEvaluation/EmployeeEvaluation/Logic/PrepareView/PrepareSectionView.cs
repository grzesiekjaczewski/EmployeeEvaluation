using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PrepareSectionView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            SurveyUserData model = Parameters as SurveyUserData;
            SurveyUserDataReturn surveyUserDataReturn = new SurveyUserDataReturn();
            SurveyPart surveyPart = new SurveyPart();

            int id = StringToValue.ParseInt(model.Id);

            surveyPart = db.T_SurveyPart.Where(sp => sp.SurveyId == id).FirstOrDefault();
            SurveyPartTemplate surveyPartTemplate = db.T_SurveyPartTemplate.Find(surveyPart.SurveyPartTemplateId);

            surveyUserDataReturn.TotalSections = db.T_SurveyPart.Where(sp => sp.SurveyId == id).Count();
            surveyUserDataReturn.TotalQuestions = db.T_SurveyPart
                .Join(db.T_SurveyQuestion,
                sp => sp.Id, sq => sq.SurveyPartId,
                (sp, sq) => sp).Where(sp => sp.SurveyId == id).Count();
            surveyUserDataReturn.TotalSectionQuestions = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id).Count();
            surveyUserDataReturn.SectionName = surveyPartTemplate.Name;
            surveyUserDataReturn.SectionTitle = surveyPartTemplate.SummaryTitle;
            surveyUserDataReturn.SectionId = surveyPart.Id;
            surveyUserDataReturn.QuestionId = 0;
            surveyUserDataReturn.SectionNo = db.T_SurveyPart.Where(sp => sp.SurveyId == id && sp.Id <= surveyPart.Id).Count();

            return surveyUserDataReturn as T1;
        }

    }
}