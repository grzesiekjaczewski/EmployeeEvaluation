using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace EmployeeEvaluation.Logic
{
    public class CopySurvey<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyPartData modelData = model as SurveyPartData;
            int id = StringToValue.ParseInt(modelData.Id);
            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            if (surveyTemplate != null)
            {
                SurveyTemplate newSurveyTemplate = new SurveyTemplate()
                {
                    Name = modelData.Name,
                    SurveyDate = DateTime.Now,
                    PublishDate = new DateTime(1900, 1, 1),
                    SurveyPartTemplates = new List<SurveyPartTemplate>()
                };

                db.Entry(surveyTemplate).Collection(p => p.SurveyPartTemplates).Load();
                if (surveyTemplate.SurveyPartTemplates != null)
                {
                    foreach (SurveyPartTemplate surveyPartTemplate in surveyTemplate.SurveyPartTemplates)
                    {
                        SurveyPartTemplate newSurveyPartTemplate = new SurveyPartTemplate()
                        {
                            Name = surveyPartTemplate.Name,
                            SummaryTitle = surveyPartTemplate.SummaryTitle,
                            SurveyQuestionTemplates = new List<SurveyQuestionTemplate>()                            
                        };

                        newSurveyTemplate.SurveyPartTemplates.Add(newSurveyPartTemplate);

                        db.Entry(surveyPartTemplate).Collection(p => p.SurveyQuestionTemplates).Load();
                        foreach (SurveyQuestionTemplate surveyQuestionTemplate in surveyPartTemplate.SurveyQuestionTemplates)
                        {
                            SurveyQuestionTemplate newSurveyQuestionTemplate = new SurveyQuestionTemplate()
                            {
                                Name = surveyQuestionTemplate.Name,
                                Definition = surveyQuestionTemplate.Definition,
                                QuestionType = surveyQuestionTemplate.QuestionType
                            };
                            newSurveyPartTemplate.SurveyQuestionTemplates.Add(newSurveyQuestionTemplate);
                        }
                    }
                }
                db.T_SurveyTemplate.Add(newSurveyTemplate);
                db.SaveChanges();
            }
        }
    }
}