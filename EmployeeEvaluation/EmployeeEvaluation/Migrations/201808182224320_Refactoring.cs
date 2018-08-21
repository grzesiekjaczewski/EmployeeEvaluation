namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactoring : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SurveyPartTemplates", "NewQuestion");
            DropColumn("dbo.SurveyTemplates", "NewPart");
            DropColumn("dbo.SurveyTemplates", "SummaryTitle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyTemplates", "SummaryTitle", c => c.String());
            AddColumn("dbo.SurveyTemplates", "NewPart", c => c.String());
            AddColumn("dbo.SurveyPartTemplates", "NewQuestion", c => c.String());
        }
    }
}
