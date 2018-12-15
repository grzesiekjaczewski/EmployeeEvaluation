using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.Remove
{
    public class RemoveEmployee : IRemoveItem
    {
        public string Save(int? id, ApplicationDbContext db)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try {

                    Employee employee = db.T_Employees.Find(id);
                    string userId = employee.UserId;
                    db.T_Employees.Remove(employee);

                    List<Survey> surveys = db.T_Survey.Where(s => s.EmployeeId == id).ToList();
                    foreach (var survey in surveys)
                    {
                        db.T_Survey.Remove(survey);
                    }

                    List <Team> teams = db.T_Teams.Where(t => t.ManagerId == id).ToList();
                    foreach(var team in teams)
                    {
                        team.ManagerId = 0;
                        db.Entry(team).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    transaction.Commit();
                    return userId;
                } 
                catch(ExecutionEngineException e)
                {
                    transaction.Rollback();
                    return "";
                }
            }
        }

    }
}