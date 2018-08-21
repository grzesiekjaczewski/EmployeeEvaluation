using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class PrepareServeyDeatilsView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            int? id = Parameters as int?;
            if (id == null)
            {
                return null;
            }

            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            db.Entry(surveyTemplate).Collection(p => p.SurveyPartTemplates).Load();
            List<SurveyPartTemplate> surveyPartTemplates = surveyTemplate.SurveyPartTemplates;
            if (surveyPartTemplates == null) surveyPartTemplates = new List<SurveyPartTemplate>();
            //List<SurveyQuestionTemplate> SurveyQuestionTemplates = new List<SurveyQuestionTemplate>();

            SurveyTemplateExtended surveyTemplateExtemded = new SurveyTemplateExtended()
            {
                Id = surveyTemplate.Id,
                Name = surveyTemplate.Name,
                SurveyDate = surveyTemplate.SurveyDate,
                SurveyPartTemplates = surveyPartTemplates,
                MamberList = new List<SurveyPartTemplateExtended>()
        };

            if (surveyTemplate.SurveyPartTemplates != null)
            {
                foreach (SurveyPartTemplate surveyPartTemplate in surveyTemplate.SurveyPartTemplates)
                {
                    SurveyPartTemplateExtended surveyPartTemplateExtended = new SurveyPartTemplateExtended()
                    {
                        Name = surveyPartTemplate.Name,
                        Id = surveyPartTemplate.Id,
                        SummaryTitle = surveyPartTemplate.SummaryTitle,
                        SurveyTemplateId = surveyPartTemplate.SurveyTemplateId,
                        CanBeDeleted = false,
                        MamberList = new List<SurveyQuestionTemplateExtended>()
                    };
                    surveyTemplateExtemded.MamberList.Add(surveyPartTemplateExtended);
                }
            }

            

            return surveyTemplateExtemded as T1;
        }
    }
}