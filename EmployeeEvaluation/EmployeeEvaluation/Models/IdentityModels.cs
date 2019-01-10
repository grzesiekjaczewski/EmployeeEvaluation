using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmployeeEvaluation.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Employee> T_Employees { get; set; }
        public DbSet<Team> T_Teams { get; set; }
        public DbSet<Position> T_Positions { get; set; }
        public DbSet<Rating> T_Ratings { get; set; }

        public DbSet<SurveyTemplate> T_SurveyTemplate { get; set; }
        public DbSet<SurveyPartTemplate> T_SurveyPartTemplate { get; set; }
        public DbSet<SurveyQuestionTemplate> T_SurveyQuestionTemplate { get; set; }
        public DbSet<Survey> T_Survey { get; set; }
        public DbSet<SurveyPart> T_SurveyPart { get; set; }
        public DbSet<SurveyQuestion> T_SurveyQuestion { get; set; }
        public DbSet<SurveySatus> T_SurveySatus { get; set; }
    }
}