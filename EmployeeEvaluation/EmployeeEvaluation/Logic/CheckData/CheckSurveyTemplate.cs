using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Logic.CheckData
{
    public class CheckSurveyTemplate<T> : ICheckData<T> where T : class
    {
        public int Check(T model, out string message, ApplicationDbContext db)
        {
            SurveyPartData modelData = model as SurveyPartData;
            int errorCode = 0;
            message = "";

            int id = StringToValue.ParseInt(modelData.Id);
            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(id);
            if (surveyTemplate != null)
            {
                db.Entry(surveyTemplate).Collection(p => p.SurveyPartTemplates).Load();
                List<SurveyPartTemplate> surveyPartTemplates = surveyTemplate.SurveyPartTemplates;
                if (surveyPartTemplates == null || surveyPartTemplates.Count() == 0)
                {
                    errorCode = 2;
                    message = "Brak przynajmniej jednej sekcji ankiety.";
                    return errorCode;
                }
                foreach (SurveyPartTemplate surveyPartTemplate in surveyTemplate.SurveyPartTemplates)
                {
                    db.Entry(surveyPartTemplate).Collection(p => p.SurveyQuestionTemplates).Load();
                    if (surveyPartTemplate.SurveyQuestionTemplates == null || surveyPartTemplate.SurveyQuestionTemplates.Count() == 0)
                    {
                        errorCode = 2;
                        message = "Sekcja '" + surveyPartTemplate.Name + "' nie zawiera ani jednego pytania.";
                        return errorCode;
                    }
                }
            }
            else
            {
                errorCode = 1;
                message = "Nie znaleziono wzorca ankiety.";
            }

            return errorCode;
        }
    }
}