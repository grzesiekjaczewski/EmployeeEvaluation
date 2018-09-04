namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SurveyTemplates", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyTemplates", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
