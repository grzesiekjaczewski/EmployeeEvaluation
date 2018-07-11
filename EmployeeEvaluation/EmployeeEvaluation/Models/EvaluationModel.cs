﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Models
{
    public class SurveyTemplate
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nazwa ankiety")]
        public string Name { get; set; }
        [Display(Name = "Data")]
        public DateTime SurveyDate { get; set; }
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

    public class Survey
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public int SurveyTemplateId { get; set; }
        [Required]
        [Display(Name = "Nazwa ankiety")]
        public string Name { get; set; }
        [Display(Name = "Data ankiety")]
        public DateTime SurveyDate { get; set; }
        [Display(Name = "Termin")]
        public DateTime SurveyDadline { get; set; }
        [Display(Name = "Wyp. pracownik")]
        public DateTime CompliteEmployeeDate { get; set; }
        [Display(Name = "Wyp. manager")]
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
}