namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieTableValues : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies(Name,GenreId,ReleaseDate,AddedDate,NumberOfStock) VALUES ('Hangover',1,'9/6/2009','5/4/2016',5)");
            Sql("INSERT INTO Movies(Name,GenreId,ReleaseDate,AddedDate,NumberOfStock) VALUES ('Die Hard',3,'9/6/2009','5/4/2016',10)");
            Sql("INSERT INTO Movies(Name,GenreId,ReleaseDate,AddedDate,NumberOfStock) VALUES ('The Terminator',3,'9/6/2009','5/4/2016',15)");
            Sql("INSERT INTO Movies(Name,GenreId,ReleaseDate,AddedDate,NumberOfStock) VALUES ('Toy Story',4,'9/6/2009','5/4/2016',25)");
            Sql("INSERT INTO Movies(Name,GenreId,ReleaseDate,AddedDate,NumberOfStock) VALUES ('Titanic',5,'9/6/2009','5/4/2016',2)");
        }
        
        public override void Down()
        {
        }
    }
}
