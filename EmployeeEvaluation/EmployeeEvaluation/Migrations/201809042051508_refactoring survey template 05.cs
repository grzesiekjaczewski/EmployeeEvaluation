namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate05 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SurveyPartTemplates", "CanBeDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyPartTemplates", "CanBeDeleted", c => c.Boolean());
        }
    }
}
