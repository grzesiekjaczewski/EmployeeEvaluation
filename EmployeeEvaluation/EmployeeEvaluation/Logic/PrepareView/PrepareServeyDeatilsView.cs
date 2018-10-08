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

            if (surveyTemplate.SurveyPartTemplates != null)
            {
                foreach (SurveyPartTemplate surveyPartTemplate in surveyTemplate.SurveyPartTemplates)
                {
                    db.Entry(surveyPartTemplate).Collection(p => p.SurveyQuestionTemplates).Load();
                }
            }

            return surveyTemplate as T1;
        }
    }
}