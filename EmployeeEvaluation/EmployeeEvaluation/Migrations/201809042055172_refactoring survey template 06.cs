namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate06 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", "dbo.SurveyTemplates");
            //DropIndex("dbo.SurveyPartTemplates", new[] { "SurveyTemplateExtended_Id" });
            //DropColumn("dbo.SurveyPartTemplates", "Discriminator");
            //DropColumn("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", c => c.Int());
            //AddColumn("dbo.SurveyPartTemplates", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            //CreateIndex("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id");
            //AddForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", "dbo.SurveyTemplates", "Id");
        }
    }
}
