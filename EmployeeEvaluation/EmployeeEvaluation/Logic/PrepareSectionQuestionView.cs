using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PrepareSectionQuestionView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            SurveyUserData model = Parameters as SurveyUserData;
            SurveyUserDataReturn surveyUserDataReturn = new SurveyUserDataReturn();

            int id = StringToValue.ParseInt(model.Id);
            int sectionId = StringToValue.ParseInt(model.SectionId);
            int questionId = StringToValue.ParseInt(model.QuestionId);

            SurveyPart surveyPart = new SurveyPart();
            SurveyQuestion surveyQuestion = new SurveyQuestion();

            surveyUserDataReturn.TotalSections = db.T_SurveyPart.Where(sp => sp.SurveyId == id).Count();
            surveyUserDataReturn.TotalQuestins = db.T_SurveyPart
                .Join(db.T_SurveyQuestion,
                sp => sp.Id, sq => sq.SurveyPartId,
                (sp, sq) => sp).Where(sp => sp.SurveyId == id).Count();

            if (sectionId == 0)
            {
                surveyPart = db.T_SurveyPart.Where(sp => sp.SurveyId == id).FirstOrDefault();
                surveyQuestion = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id).FirstOrDefault();
                surveyUserDataReturn.SectionNo = 1;
                surveyUserDataReturn.QuestionNo = 1;
                surveyUserDataReturn.TotalSectionQuestins = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id).Count();
            }
            else
            {
                if (model.NextPrev == "n")
                {
                    int nextSectionQuestins = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == sectionId && sq.Id > sectionId).Count();
                    if (nextSectionQuestins == 0)
                    {
                        if (db.T_SurveyPart.Where(sp => sp.SurveyId == id && sp.Id > sectionId).Count() > 0)
                        {
                            surveyPart = db.T_SurveyPart.Where(sp => sp.SurveyId == id && sp.Id > sectionId).FirstOrDefault();
                        }
                        else
                        {
                            surveyPart = db.T_SurveyPart.Where(sp => sp.Id == sectionId).FirstOrDefault();
                        }

                        surveyUserDataReturn.SectionNo = db.T_SurveyPart.Where(sp => sp.SurveyId == id && sp.Id <= surveyPart.Id).Count();

                        if (db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id && sq.Id > questionId).Count() > 0)
                        {
                            surveyQuestion = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id && sq.Id > questionId).FirstOrDefault();
                        }
                        else
                        {
                            surveyQuestion = db.T_SurveyQuestion.Where(sq => sq.Id == questionId).FirstOrDefault();
                        }

                        surveyUserDataReturn.QuestionNo = db.T_SurveyQuestion.Where(sq => sq.SurveyPartId == surveyPart.Id && sq.Id <= surveyQuestion.Id).Count();
                    }


                }
                else
                {

                }
            }

            SurveyPartTemplate surveyPartTemplate = db.T_SurveyPartTemplate.Find(surveyPart.SurveyPartTemplateId);
            SurveyQuestionTemplate surveyQuestionTemplate = db.T_SurveyQuestionTemplate.Find(surveyQuestion.SurveyQuestionTemplateId);

            surveyUserDataReturn.SectionId = surveyPart.Id;
            surveyUserDataReturn.QuestinId = surveyQuestion.Id;

            surveyUserDataReturn.SectionName = surveyPartTemplate.Name;
            surveyUserDataReturn.SectionTitle = surveyPartTemplate.SummaryTitle;
            surveyUserDataReturn.SectionEmployeeSummary = surveyPart.EmployeeSummary;
            surveyUserDataReturn.SectionEmployeeScore = surveyPart.ManagerSummaryScore;
            surveyUserDataReturn.QuestionName = surveyQuestionTemplate.Name;
            surveyUserDataReturn.QuestionDescription = surveyQuestionTemplate.Definition;
            surveyUserDataReturn.QuestionType = surveyQuestionTemplate.QuestionType;
            surveyUserDataReturn.QuestionEmployeeScore = surveyQuestion.EmployeeScore;
            surveyUserDataReturn.QuestionEmployeeComment = surveyQuestion.EmployeeComment;

            return surveyUserDataReturn as T1;
        }
    }
}