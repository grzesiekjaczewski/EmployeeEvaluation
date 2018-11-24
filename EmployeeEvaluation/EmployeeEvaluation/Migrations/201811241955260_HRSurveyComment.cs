namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HRSurveyComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surveys", "HRSummary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surveys", "HRSummary");
        }
    }
}
