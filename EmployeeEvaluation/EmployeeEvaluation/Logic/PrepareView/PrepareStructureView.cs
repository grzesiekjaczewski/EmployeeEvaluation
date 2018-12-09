using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.PrepareView
{
    public class PrepareStructureView<T1> : IPrepareView<T1> where T1 : class
    {
        Structure _structure;

        public T1 GetView(ApplicationDbContext db)
        {
            Structure structure = new Structure();
            _structure = structure;

            structure.Boss = (from e in db.T_Employees
                              join t in db.T_Teams on e.TeamId equals t.Id
                              join p in db.T_Positions on e.PositionId equals p.Id
                              where t.ManagerId == e.Id
                              && t.Id != 1
                              select new Boss
                              {
                                  Id = e.Id,
                                  FirstName = e.FirstName,
                                  LastName = e.LastName,
                                  Position = p.Name,
                                  MyTeam = t.Name
                              }
                              ).ToList()[0];

            structure.Boss.Teams = (from t in db.T_Teams
                                    where t.ManagerId == structure.Boss.Id
                                    select new MyTeam
                                     {
                                        Id = t.Id,
                                        Name = t.Name
                                     }
                                    ).ToList();

            _structure.PersonCount = 0;
            _structure.TeamCount = 0;

            getMyEmployee(structure.Boss, db);

            return structure as T1;
        }

        private void getMyEmployee(Person person, ApplicationDbContext db)
        {
            _structure.PersonCount += 1;
            foreach (var team in person.Teams)
            {
                _structure.TeamCount += 1;
                team.Persons = (from e in db.T_Employees
                                join tm in db.T_Teams on e.TeamId equals tm.Id
                                join p in db.T_Positions on e.PositionId equals p.Id
                                where tm.ManagerId == person.Id && e.Id != person.Id && e.TeamId == team.Id
                                select new Person
                                {
                                    Id = e.Id,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    Position = p.Name,
                                    MyTeam = tm.Name,
                                    Teams = (from t in db.T_Teams
                                             where t.ManagerId == e.Id
                                             select new MyTeam
                                             {
                                                 Id = t.Id,
                                                 Name = t.Name
                                             }
                                            ).ToList()
                                }
                               ).ToList();
                foreach (var p in team.Persons)
                {
                    getMyEmployee(p, db);
                }
            }
        }
    }
}