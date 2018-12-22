using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeEvaluation.Models
{
    public class PersonalData
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string EMail { get; set; }
        public string ErrorMessage { get; set; }
        public bool CanSave { get; set; }

    }

    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public int TeamId { get; set; }
        public int PositionId { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Data zatrudnienia")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

    }

    public class EmployeeExtended
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Display(Name = "Zespół")]
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        [Display(Name = "Stanowisko")]
        public string PositionName { get; set; }
        public int PositionId { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "E-Mail")]
        public string EMail { get; set; }
        public bool IsManager { get; set; }
        [Display(Name = "Menadżer")]
        public string Manager { get; set; }
        [Display(Name = "Data zatrudnienia")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
        [Display(Name = "Status")]
        public int Status { get; set; }
        [Display(Name = "Status")]
        public string StatusName { get; set; }
    }

    public class ManagerStructure
    {
        public string ManagerName { get; set; }
        public string PositionName { get; set; }
        public List<TeamExtended> Teams { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Zespół")]
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class TeamExtended
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Zespół")]
        public string Name { get; set; }
        public int ManagerId { get; set; }
        [Display(Name = "Menadżer")]
        public string ManagerName { get; set; }
        public int Members { get; set; }
        public List<EmployeeExtended> MamberList { get; set; }
    }

    public class TeamStructure
    {
        public TeamExtended TeamExtended { get; set; }
        public List<EmployeeExtended> TeamMembers { get; set; }
    }


    public class Position
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Stanowisko")]
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class PositionExtended
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Stanowisko")]
        public string Name { get; set; }
        public int Members { get; set; }
    }

    public class Rating
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nota")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }

}