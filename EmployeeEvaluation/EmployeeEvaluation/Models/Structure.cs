using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Models
{
    public class Structure
    {
        public Boss Boss { get; set; }
    }

    public class Boss : Persson
    {
        public List<Persson> Employees { get; set; }
    }

    public class Persson
    {
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Stanowisko")]
        public string Position { get; set; }
        [Display(Name = "Zsspół")]
        public string Team { get; set; }
        public List<Persson> Employees { get; set; }
    }

}