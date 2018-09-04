namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringsurveytemplate04 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.SurveyPartTemplates", "CanBeDeleted", c => c.Boolean());
            //AddColumn("dbo.SurveyPartTemplates", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            //AddColumn("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", c => c.Int());
            //CreateIndex("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id");
            //AddForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", "dbo.SurveyTemplates", "Id");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id", "dbo.SurveyTemplates");
            //DropIndex("dbo.SurveyPartTemplates", new[] { "SurveyTemplateExtended_Id" });
            //DropColumn("dbo.SurveyPartTemplates", "SurveyTemplateExtended_Id");
            //DropColumn("dbo.SurveyPartTemplates", "Discriminator");
            //DropColumn("dbo.SurveyPartTemplates", "CanBeDeleted");
        }
    }
}
