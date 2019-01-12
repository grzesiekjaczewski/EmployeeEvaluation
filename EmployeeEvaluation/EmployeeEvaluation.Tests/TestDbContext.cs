using EmployeeEvaluation.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace EmployeeEvaluation.Tests.Logic
{
    public class TestDbContext : DbContext
    {
        public static TestDbContext Create()
        {
            return new TestDbContext();
        }

        public virtual DbSet<Employee> T_Employees { get; set; }
        public virtual DbSet<Team> T_Teams { get; set; }
        public virtual DbSet<Position> T_Positions { get; set; }
        public virtual DbSet<Rating> T_Ratings { get; set; }

        public virtual DbSet<SurveyTemplate> T_SurveyTemplate { get; set; }
        public virtual DbSet<SurveyPartTemplate> T_SurveyPartTemplate { get; set; }
        public virtual DbSet<SurveyQuestionTemplate> T_SurveyQuestionTemplate { get; set; }
        public virtual DbSet<Survey> T_Survey { get; set; }
        public virtual DbSet<SurveyPart> T_SurveyPart { get; set; }
        public virtual DbSet<SurveyQuestion> T_SurveyQuestion { get; set; }
        public virtual DbSet<SurveySatus> T_SurveySatus { get; set;}
    }
}
