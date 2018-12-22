namespace EmployeeEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HireDate3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime(nullable: true));
        }
    }
}
