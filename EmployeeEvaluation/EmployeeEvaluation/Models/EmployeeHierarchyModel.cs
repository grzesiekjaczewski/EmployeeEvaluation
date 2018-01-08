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
        public bool IsManager { get; set; }
        public int TeamId { get; set; }
        public int PositionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class Position
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}