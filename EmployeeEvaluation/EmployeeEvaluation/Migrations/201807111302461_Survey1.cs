namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Survey1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PositionEmployees", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.PositionEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.TeamEmployees", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TeamEmployees", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.PositionEmployees", new[] { "Position_Id" });
            DropIndex("dbo.PositionEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.TeamEmployees", new[] { "Team_Id" });
            DropIndex("dbo.TeamEmployees", new[] { "Employee_Id" });
            CreateIndex("dbo.Employees", "TeamId");
            CreateIndex("dbo.Employees", "PositionId");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            DropTable("dbo.PositionEmployees");
            DropTable("dbo.TeamEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamEmployees",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Employee_Id });
            
            CreateTable(
                "dbo.PositionEmployees",
                c => new
                    {
                        Position_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Position_Id, t.Employee_Id });
            
            DropForeignKey("dbo.Employees", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.Employees", new[] { "TeamId" });
            CreateIndex("dbo.TeamEmployees", "Employee_Id");
            CreateIndex("dbo.TeamEmployees", "Team_Id");
            CreateIndex("dbo.PositionEmployees", "Employee_Id");
            CreateIndex("dbo.PositionEmployees", "Position_Id");
            AddForeignKey("dbo.TeamEmployees", "Employee_Id", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamEmployees", "Team_Id", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PositionEmployees", "Employee_Id", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PositionEmployees", "Position_Id", "dbo.Positions", "Id", cascadeDelete: true);
        }
    }
}
