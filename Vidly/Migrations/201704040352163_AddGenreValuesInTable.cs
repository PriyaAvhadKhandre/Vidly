namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreValuesInTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres(Id,Name) VALUES (1,'Comedy')");
            Sql("INSERT INTO Genres(Id,Name) VALUES (2,'Horror')");
            Sql("INSERT INTO Genres(Id,Name) VALUES (3,'Action')");
            Sql("INSERT INTO Genres(Id,Name) VALUES (4,'Family')");
            Sql("INSERT INTO Genres(Id,Name) VALUES (5,'Romance')");
            Sql("INSERT INTO Genres(Id,Name) VALUES (6,'Drama')");
        }
        
        public override void Down()
        {
        }
    }
}
