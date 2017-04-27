namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerTableBirthdateColumn : DbMigration
    {
        public override void Up()
        {
            Sql("Update dbo.Customers set Birthdate='1/1/1980' where Id = 1");
        }
        
        public override void Down()
        {
        }
    }
}
