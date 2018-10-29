using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Models
{
    public class SurveyUserData
    {
        public string Id { get; set; }
        public string SectionId { get; set; }
        public string QuestionId { get; set; }
        public string QuestionSelection { get; set; }
        public string Page { get; set; }
        public string NextPrev { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string QuestionEmployeeComment { get; set; }
    }

    public class SurveyUserDataReturn
    {
        public int TotalQuestions { get; set; }
        public int TotalSections { get; set; }
        public int TotalSectionQuestions { get; set; }
        public int QuestionId { get; set; }
        public int SectionId { get; set; }
        public int QuestionNo { get; set; }
        public int QuestionSectionNo { get; set; }
        public int SectionNo { get; set; }
        public string SectionName { get; set; }
        public string SectionTitle { get; set; }
        public string SectionEmployeeSummary { get; set; }
        public decimal SectionEmployeeScore { get; set; }
        public string QuestionName { get; set; }
        public string QuestionDescription { get; set; }
        public int QuestionType { get; set; }
        public int QuestionEmployeeScore { get; set; }
        public string QuestionEmployeeComment { get; set; }


    }


    public class SurveyQuestionData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Criteria { get; set; }
        public string Type { get; set; }
    }

    public class SurveyPartData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
    }

    public class SurveyTemplate
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nazwa ankiety")]
        public string Name { get; set; }
        [Display(Name = "Data utworzenia")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SurveyDate { get; set; }
        [Display(Name = "Data publikacji")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        public List<SurveyPartTemplate> SurveyPartTemplates { get; set; }
    }

    public class SurveyPartTemplate
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tytuł części")]
        public string Name { get; set; }
        public int SurveyTemplateId { get; set; }
        [Display(Name = "Podsumowanie")]
        public string SummaryTitle { get; set; }
        public List<SurveyQuestionTemplate> SurveyQuestionTemplates { get; set; }
    }


    public class SurveyQuestionTemplate
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Pytanie")]
        public string Name { get; set; }
        [Display(Name = "Definicja")]
        public string Definition { get; set; }
        public int SurveyPartTemplateId { get; set; }
        public int QuestionType { get; set; }
    }

    public class SurveyDisplay
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa ankiety")]
        public string Name { get; set; }
        [Display(Name = "Termin")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SurveyDadline { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        public bool EmployeeCompleted { get; set; }
    }


    public class Survey
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public int SurveyTemplateId { get; set; }
        public int SurveyStatusId { get; set; }
        [Required]
        [Display(Name = "Nazwa ankiety")]
        public string Name { get; set; }
        [Display(Name = "Data ankiety")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SurveyDate { get; set; }
        [Display(Name = "Termin")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SurveyDadline { get; set; }
        [Display(Name = "Wyp. pracownik")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CompliteEmployeeDate { get; set; }
        [Display(Name = "Wyp. manager")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CompliteManagerDate { get; set; }
        public string EmployeeSummary { get; set; }
        public decimal EmployeeSummaryScore { get; set; }
        public string ManagerSummary { get; set; }
        public decimal ManagerSummaryScore { get; set; }
        public bool EmployeeCompleted { get; set; }
        public bool ManagerCompleted { get; set; }
        public List<SurveyPart> SurveyParts { get; set; }
    }

    public class SurveyPart
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int SurveyPartTemplateId { get; set; }
//        [Required]
//       [Display(Name = "Tytuł części")]
//        public string Name { get; set; }
//        [Display(Name = "Podsumowanie")]
//        public string SummaryTitle { get; set; }
        public string EmployeeSummary { get; set; }
        public decimal EmployeeSummaryScore { get; set; }
        public string ManagerSummary { get; set; }
        public decimal ManagerSummaryScore { get; set; }
        public List<SurveyQuestion> SurveyQuestions { get; set; }
    }

    public class SurveyQuestion
    {
        public int Id { get; set; }
        public int SurveyPartId { get; set; }
        public int SurveyQuestionTemplateId { get; set; }
//        [Display(Name = "Pytanie")]
//        public string Name { get; set; }
//        public int QuestionType { get; set; }
//        public string CommentTitle { get; set; }
        public string EmployeeComment { get; set; }
        public int EmployeeScore { get; set; }
        public string ManagerComment { get; set; }
        public int ManagerScore { get; set; }
    }

    public class SurveySatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}