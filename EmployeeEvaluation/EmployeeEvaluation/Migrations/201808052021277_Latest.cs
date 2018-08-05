namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Latest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyPartTemplates", "CanBeDeleted", c => c.Boolean());
            AddColumn("dbo.SurveyPartTemplates", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.SurveyPartTemplates", "SurveyTemplateExtemded_Id", c => c.Int());
            AddColumn("dbo.SurveyQuestionTemplates", "CanBeDeleted", c => c.Boolean());
            AddColumn("dbo.SurveyQuestionTemplates", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id", c => c.Int());
            AddColumn("dbo.SurveyTemplates", "CanBeDeleted", c => c.Boolean());
            AddColumn("dbo.SurveyTemplates", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.SurveyPartTemplates", "SurveyTemplateExtemded_Id");
            CreateIndex("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id");
            AddForeignKey("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id", "dbo.SurveyPartTemplates", "Id");
            AddForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateExtemded_Id", "dbo.SurveyTemplates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateExtemded_Id", "dbo.SurveyTemplates");
            DropForeignKey("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id", "dbo.SurveyPartTemplates");
            DropIndex("dbo.SurveyQuestionTemplates", new[] { "SurveyPartTemplateExtended_Id" });
            DropIndex("dbo.SurveyPartTemplates", new[] { "SurveyTemplateExtemded_Id" });
            DropColumn("dbo.SurveyTemplates", "Discriminator");
            DropColumn("dbo.SurveyTemplates", "CanBeDeleted");
            DropColumn("dbo.SurveyQuestionTemplates", "SurveyPartTemplateExtended_Id");
            DropColumn("dbo.SurveyQuestionTemplates", "Discriminator");
            DropColumn("dbo.SurveyQuestionTemplates", "CanBeDeleted");
            DropColumn("dbo.SurveyPartTemplates", "SurveyTemplateExtemded_Id");
            DropColumn("dbo.SurveyPartTemplates", "Discriminator");
            DropColumn("dbo.SurveyPartTemplates", "CanBeDeleted");
        }
    }
}
