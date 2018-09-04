namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate02 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SurveyQuestionTemplates", "CanBeDeleted");
            DropColumn("dbo.SurveyQuestionTemplates", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyQuestionTemplates", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.SurveyQuestionTemplates", "CanBeDeleted", c => c.Boolean());
        }
    }
}
