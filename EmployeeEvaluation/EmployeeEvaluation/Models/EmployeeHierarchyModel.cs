using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EmployeeEvaluation.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public int TeamId { get; set; }
        public List<Team> Teams { get; set; }
        public int PositionId { get; set; }
        public List<Position> Positions { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
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
        [Display(Description = "Opis")]
        public string Description { get; set; }
    }

}