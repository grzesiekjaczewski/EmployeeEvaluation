namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Survey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyTemplates", "SummaryTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyTemplates", "SummaryTitle");
        }
    }
}
