namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipTypeNameColumn : DbMigration
    {
        public override void Up()
        {
            Sql("Update dbo.MembershipTypes set Name = 'Pay as You Go' where SignUpFee = 0 and DiscountRate = 0");
            Sql("Update dbo.MembershipTypes set Name = 'Monthly' where SignUpFee = 30 and DiscountRate = 10");
            Sql("Update dbo.MembershipTypes set Name = 'Quaterly' where SignUpFee = 90 and DiscountRate = 15");
            Sql("Update dbo.MembershipTypes set Name = 'Yearly' where SignUpFee = 300 and DiscountRate = 20");
        }
        
        public override void Down()
        {
        }
    }
}
