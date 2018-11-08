using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public class CreateUserEmployee
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public void Create(string userName, string userId)
        {
            userName = userName.Trim();
            Employee employee = new Employee
            {
                UserId = userId,
                FirstName = extractFirstName(userName),
                LastName = extractLastName(userName),
                PositionId = 1,
                TeamId = 1                
            };

            _db.T_Employees.Add(employee);
            _db.SaveChanges();
        }

        private string extractLastName(string userName)
        {
            if (userName.IndexOf(" ") > 0)
            {
                return userName.Substring(userName.IndexOf(" ") + 1, userName.Length - userName.IndexOf(" ") - 1);
            }

            return "";
        }

        private string extractFirstName(string userName)
        {
            if (userName.LastIndexOf(" ") > 0)
            {
                return userName.Substring(0, userName.IndexOf(" "));
            }
            return "";
        }
    }
}