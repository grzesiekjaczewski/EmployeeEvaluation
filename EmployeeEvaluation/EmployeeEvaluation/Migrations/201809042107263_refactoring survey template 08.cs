namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate08 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SurveyTemplates", "CanBeDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyTemplates", "CanBeDeleted", c => c.Boolean());
        }
    }
}
