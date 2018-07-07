namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratings3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "TeamId", "dbo.Teams");
            DropIndex("dbo.Employees", new[] { "TeamId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            CreateTable(
                "dbo.PositionEmployees",
                c => new
                    {
                        Position_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Position_Id, t.Employee_Id })
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Position_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.TeamEmployees",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Employee_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.TeamEmployees", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.PositionEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.PositionEmployees", "Position_Id", "dbo.Positions");
            DropIndex("dbo.TeamEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.TeamEmployees", new[] { "Team_Id" });
            DropIndex("dbo.PositionEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.PositionEmployees", new[] { "Position_Id" });
            DropTable("dbo.TeamEmployees");
            DropTable("dbo.PositionEmployees");
            CreateIndex("dbo.Employees", "PositionId");
            CreateIndex("dbo.Employees", "TeamId");
            AddForeignKey("dbo.Employees", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
        }
    }
}
