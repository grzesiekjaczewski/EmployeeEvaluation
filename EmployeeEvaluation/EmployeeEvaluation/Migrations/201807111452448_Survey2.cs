namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Survey2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        SurveyTemplateId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        SurveyDate = c.DateTime(nullable: false),
                        SurveyDadline = c.DateTime(nullable: false),
                        CompliteEmployeeDate = c.DateTime(nullable: false),
                        CompliteManagerDate = c.DateTime(nullable: false),
                        EmployeeSummary = c.String(),
                        EmployeeSummaryScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManagerSummary = c.String(),
                        ManagerSummaryScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeCompleted = c.Boolean(nullable: false),
                        ManagerCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SurveyParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        SurveyPartTemplateId = c.Int(nullable: false),
                        EmployeeSummary = c.String(),
                        EmployeeSummaryScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManagerSummary = c.String(),
                        ManagerSummaryScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId);
            
            CreateTable(
                "dbo.SurveyQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyPartId = c.Int(nullable: false),
                        SurveyQuestionTemplateId = c.Int(nullable: false),
                        EmployeeComment = c.String(),
                        EmployeeScore = c.Int(nullable: false),
                        ManagerComment = c.String(),
                        ManagerScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyParts", t => t.SurveyPartId, cascadeDelete: true)
                .Index(t => t.SurveyPartId);
            
            CreateTable(
                "dbo.SurveyPartTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SurveyTemplateId = c.Int(nullable: false),
                        SummaryTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyTemplates", t => t.SurveyTemplateId, cascadeDelete: true)
                .Index(t => t.SurveyTemplateId);
            
            CreateTable(
                "dbo.SurveyQuestionTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Definition = c.String(),
                        SurveyPartTemplateId = c.Int(nullable: false),
                        QuestionType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyPartTemplates", t => t.SurveyPartTemplateId, cascadeDelete: true)
                .Index(t => t.SurveyPartTemplateId);
            
            CreateTable(
                "dbo.SurveyTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SurveyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyPartTemplates", "SurveyTemplateId", "dbo.SurveyTemplates");
            DropForeignKey("dbo.SurveyQuestionTemplates", "SurveyPartTemplateId", "dbo.SurveyPartTemplates");
            DropForeignKey("dbo.SurveyParts", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.SurveyQuestions", "SurveyPartId", "dbo.SurveyParts");
            DropIndex("dbo.SurveyQuestionTemplates", new[] { "SurveyPartTemplateId" });
            DropIndex("dbo.SurveyPartTemplates", new[] { "SurveyTemplateId" });
            DropIndex("dbo.SurveyQuestions", new[] { "SurveyPartId" });
            DropIndex("dbo.SurveyParts", new[] { "SurveyId" });
            DropTable("dbo.SurveyTemplates");
            DropTable("dbo.SurveyQuestionTemplates");
            DropTable("dbo.SurveyPartTemplates");
            DropTable("dbo.SurveyQuestions");
            DropTable("dbo.SurveyParts");
            DropTable("dbo.Surveys");
        }
    }
}
