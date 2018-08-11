namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nothingimportant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyPartTemplates", "NewQuestion", c => c.String());
            AddColumn("dbo.SurveyTemplates", "NewPart", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyTemplates", "NewPart");
            DropColumn("dbo.SurveyPartTemplates", "NewQuestion");
        }
    }
}
