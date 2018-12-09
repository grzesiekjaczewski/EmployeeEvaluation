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
        public int TeamCount { get; set; }
        public int PersonCount { get; set; }
    }

    public class Boss : Person
    {
    }

    public class Person
    {
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Stanowisko")]
        public string Position { get; set; }
        [Display(Name = "Zespół")]
        public string MyTeam { get; set; }
        public List<MyTeam> Teams { get; set; }
    }

    public class MyTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> Persons { get; set; }
    }

}