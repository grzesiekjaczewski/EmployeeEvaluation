using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PrepareSurveyBrowseView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            int? id = Parameters as int?;

            Survey survey = db.T_Survey.Find(id);
            SurveyTemplate surveyTemplate = db.T_SurveyTemplate.Find(survey.SurveyTemplateId);

            db.Entry(survey).Collection(p => p.SurveyParts).Load();
            db.Entry(surveyTemplate).Collection(p => p.SurveyPartTemplates).Load();
            foreach (SurveyPart surveyPart in survey.SurveyParts)
            {
                db.Entry(surveyPart).Collection(p => p.SurveyQuestions).Load();
            }

            foreach (SurveyPartTemplate surveyPartTemplate in surveyTemplate.SurveyPartTemplates)
            {
                db.Entry(surveyPartTemplate).Collection(p => p.SurveyQuestionTemplates).Load();
            }

            BrowseSurvey browseSurvey = new BrowseSurvey()
            {
                Survey = survey,
                SurveyTemplate = surveyTemplate
            };

            return browseSurvey as T1;
        }
    }
}
