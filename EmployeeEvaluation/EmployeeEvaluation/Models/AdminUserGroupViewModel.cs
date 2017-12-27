using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool ActiveForUser { get; set; }
    }

    public class UsersAndRights
    {
        public string Id { get; set;}
        [Display(Name = "Użytkownik")]
        public string UserName { get; set; }
        [Display(Name = "Adm.")]
        public bool IsAdmin { get; set; }
        [Display(Name = "HR Man.")]
        public bool IsHRManager { get; set; }
        [Display(Name = "Man.")]
        public bool IsManager { get; set; }
        [Display(Name = "Prac.")]
        public bool IsEmployee { get; set; }
    }
}