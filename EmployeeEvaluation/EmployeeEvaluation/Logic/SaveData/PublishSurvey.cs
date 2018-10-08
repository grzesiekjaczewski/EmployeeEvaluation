using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace EmployeeEvaluation.Logic
{
    public class PublishSurvey<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            SurveyPartData modelData = model as SurveyPartData;

            int id = StringToValue.ParseInt(modelData.Id);
            DateTime deadline = CalculateDate.StringToDate(modelData.Summary, ".");

            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            if (surveyTemplate != null)
            {
                List<Survey> surveys = new List<Survey>();
                List<Employee> employees = db.T_Employees.ToList();

                db.Entry(surveyTemplate).Collection(p => p.SurveyPartTemplates).Load();
                List<SurveyPartTemplate> surveyPartTemplates = surveyTemplate.SurveyPartTemplates;
                if (surveyPartTemplates == null) surveyPartTemplates = new List<SurveyPartTemplate>();


                foreach (Employee employee in employees)
                {
                    Team team = db.T_Teams.Find(employee.TeamId);
                    Survey survey = new Survey()
                    {
                        EmployeeId = employee.Id,
                        ManagerId = team.ManagerId,
                        SurveyTemplateId = id,
                        SurveyStatusId = 1,
                        Name = modelData.Name,
                        SurveyDate = DateTime.Now,
                        SurveyDadline = deadline,
                        EmployeeCompleted = false,
                        ManagerCompleted = false,
                        CompliteEmployeeDate = new DateTime(1901, 1, 1),
                        CompliteManagerDate = new DateTime(1901, 1, 1),
                        SurveyParts = new List<SurveyPart>()
                    };

                    if (surveyTemplate.SurveyPartTemplates != null)
                    {
                        foreach (SurveyPartTemplate surveyPartTemplate in surveyTemplate.SurveyPartTemplates)
                        {
                            SurveyPart surveyPart = new SurveyPart()
                            {
                                SurveyPartTemplateId = surveyPartTemplate.Id,
                                SurveyQuestions = new List<SurveyQuestion>()
                            };

                            survey.SurveyParts.Add(surveyPart);

                            db.Entry(surveyPartTemplate).Collection(p => p.SurveyQuestionTemplates).Load();
                            foreach (SurveyQuestionTemplate surveyQuestionTemplate in surveyPartTemplate.SurveyQuestionTemplates)
                            {
                                SurveyQuestion surveyQuestion = new SurveyQuestion()
                                {
                                    SurveyQuestionTemplateId = surveyQuestionTemplate.Id
                                };
                                surveyPart.SurveyQuestions.Add(surveyQuestion);
                            }
                        }
                    }
                    surveys.Add(survey);
                }
                db.T_Survey.AddRange(surveys);
                db.SaveChanges();
            }
        }
    }
}