using EmployeeEvaluation.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PreparePrevMGSectionView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            SurveyUserData model = Parameters as SurveyUserData;
            SurveyUserDataReturn surveyUserDataReturn = new SurveyUserDataReturn();

            int id = StringToValue.ParseInt(model.Id);
            int surveyPartId = StringToValue.ParseInt(model.SectionId);

            List<SurveyPart> surveyParts = db.T_SurveyPart.Where(sp => sp.SurveyId == id && sp.Id < surveyPartId).ToList();
            SurveyPart surveyPart = surveyParts[surveyParts.Count() - 1];

            List<SurveyQuestion> surveyQuestions = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id).ToList();
            SurveyQuestion surveyQuestion = surveyQuestions[surveyQuestions.Count() - 1];

            SurveyPartTemplate surveyPartTemplate = db.T_SurveyPartTemplate.Find(surveyPart.SurveyPartTemplateId);
            SurveyQuestionTemplate surveyQuestionTemplate = db.T_SurveyQuestionTemplate.Find(surveyQuestion.SurveyQuestionTemplateId);

            surveyUserDataReturn.TotalSections = db.T_SurveyPart.Where(sp => sp.SurveyId == id).Count();
            surveyUserDataReturn.TotalQuestions = db.T_SurveyPart
                .Join(db.T_SurveyQuestion,
                sp => sp.Id, sq => sq.SurveyPartId,
                (sp, sq) => sp).Where(sp => sp.SurveyId == id).Count();
            surveyUserDataReturn.TotalSectionQuestions = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id).Count();
            surveyUserDataReturn.QuestionSectionNo = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id && sq.Id <= surveyQuestion.Id).Count();
            surveyUserDataReturn.SectionName = surveyPartTemplate.Name;
            surveyUserDataReturn.SectionTitle = surveyPartTemplate.SummaryTitle;
            surveyUserDataReturn.SectionId = surveyPart.Id;
            surveyUserDataReturn.QuestionId = surveyQuestion.Id;
            surveyUserDataReturn.QuestionName = surveyQuestionTemplate.Name;
            surveyUserDataReturn.QuestionDescription = surveyQuestionTemplate.Definition;
            surveyUserDataReturn.QuestionType = surveyQuestionTemplate.QuestionType;
            surveyUserDataReturn.QuestionEmployeeScore = surveyQuestion.ManagerScore;
            surveyUserDataReturn.QuestionEmployeeComment = surveyQuestion.ManagerComment;
            surveyUserDataReturn.SectionNo = db.T_SurveyPart.Where(sp => sp.SurveyId == id && sp.Id <= surveyPart.Id).Count();

            return surveyUserDataReturn as T1;
        }
    }
}