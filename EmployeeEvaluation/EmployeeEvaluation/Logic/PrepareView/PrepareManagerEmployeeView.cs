using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PrepareManagerEmployeeView<T1, T2> : IPrepareExtendedView<T1, T2> where T1 : class
    {
        public T2 Parameters { get; set; }

        public T1 GetView(ApplicationDbContext db)
        {
            string userId = Parameters as string;
            Employee manager = db.T_Employees.Where(m => m.UserId == userId).FirstOrDefault();
            List<EmployeeExtended> employees = new List<EmployeeExtended>();
            if (manager != null)
            {
                Team team = db.T_Teams.Where(t => t.ManagerId == manager.Id).FirstOrDefault();
                employees = db.T_Employees.Where(e => e.TeamId == team.Id && e.UserId != userId).Select(e => new EmployeeExtended()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PositionName = db.T_Positions.Where(p => p.Id == e.PositionId).FirstOrDefault().Name,
                    TeamName = team.Name,
                    Status = (
                            db.T_Survey.Where(s => s.EmployeeId == e.Id && s.SurveyStatusId == 1).ToList().Count() > 0
                            ? 1
                            : db.T_Survey.Where(s => s.EmployeeId == e.Id && s.SurveyStatusId == 2).ToList().Count() > 0
                            ? 2
                            : db.T_Survey.Where(s => s.EmployeeId == e.Id && s.SurveyStatusId == 3).ToList().Count() > 0
                            ? 3
                            : db.T_Survey.Where(s => s.EmployeeId == e.Id && s.SurveyStatusId == 4).ToList().Count() > 0
                            ? 4
                            : 0
                        ),
                    StatusName = (
                            db.T_Survey.Where(s => s.EmployeeId == e.Id && s.SurveyStatusId == 1).ToList().Count() > 0
                            ? "Oczekująca"
                            : db.T_Survey.Where(s => s.EmployeeId == e.Id && s.SurveyStatusId == 2).ToList().Count() > 0
                            ? "Wypelniona"
                            : db.T_Survey.Where(s => s.EmployeeId == e.Id && s.SurveyStatusId == 3).ToList().Count() > 0
                            ? "Oceniona"
                            : db.T_Survey.Where(s => s.EmployeeId == e.Id && s.SurveyStatusId == 4).ToList().Count() > 0
                            ? "Zaakceptowana"
                            : ""
                        )
                }
                ).ToList();
            }
            return employees as T1;
        }
    }
}