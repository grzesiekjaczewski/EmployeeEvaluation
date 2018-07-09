namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratings31 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "IsManager");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "IsManager", c => c.Boolean(nullable: false));
        }
    }
}
