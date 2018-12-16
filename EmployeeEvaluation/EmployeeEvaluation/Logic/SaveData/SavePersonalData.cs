using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.SaveData
{
    public class SavePersonalData<T> : ISaveModel<T> where T : class
    {
        public void Save(T model, ApplicationDbContext db)
        {
            PersonalData personalData = model as PersonalData;
            personalData.ErrorMessage = "Podany email już jest już zajęty";

        }
    }
}