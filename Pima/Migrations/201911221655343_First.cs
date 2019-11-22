namespace Pima.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PROGRAMMER.Article",
                c => new
                    {
                        ArticleId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ArticleId);
            
            CreateTable(
                "PROGRAMMER.ArticlesUser",
                c => new
                    {
                        ArticlesUserId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ArticleId_ArticlesUser = c.Decimal(precision: 10, scale: 0),
                        UserId_ArticlesUser = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ArticlesUserId)
                .ForeignKey("PROGRAMMER.Article", t => t.ArticleId_ArticlesUser)
                .ForeignKey("PROGRAMMER.User", t => t.UserId_ArticlesUser)
                .Index(t => t.ArticleId_ArticlesUser)
                .Index(t => t.UserId_ArticlesUser);
            
            CreateTable(
                "PROGRAMMER.User",
                c => new
                    {
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Login = c.String(maxLength: 50),
                        Password = c.String(maxLength: 100),
                        Foto = c.Binary(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "PROGRAMMER.NotesUser",
                c => new
                    {
                        NotesUserId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NotesId_NotesUser = c.Decimal(precision: 10, scale: 0),
                        UserId_NotesUser = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.NotesUserId)
                .ForeignKey("PROGRAMMER.Notes", t => t.NotesId_NotesUser)
                .ForeignKey("PROGRAMMER.User", t => t.UserId_NotesUser)
                .Index(t => t.NotesId_NotesUser)
                .Index(t => t.UserId_NotesUser);
            
            CreateTable(
                "PROGRAMMER.Notes",
                c => new
                    {
                        NotesId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 500),
                        Author = c.String(maxLength: 100),
                        Note = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.NotesId);
            
            CreateTable(
                "PROGRAMMER.SongsUser",
                c => new
                    {
                        SongsUserId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SongId_SongsUser = c.Decimal(precision: 10, scale: 0),
                        UserId_SongsUser = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.SongsUserId)
                .ForeignKey("PROGRAMMER.Songs", t => t.SongId_SongsUser)
                .ForeignKey("PROGRAMMER.User", t => t.UserId_SongsUser)
                .Index(t => t.SongId_SongsUser)
                .Index(t => t.UserId_SongsUser);
            
            CreateTable(
                "PROGRAMMER.Songs",
                c => new
                    {
                        SongsId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 500),
                        Author = c.String(maxLength: 100),
                        Text = c.String(),
                        Music = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SongsId);
            
            CreateTable(
                "PROGRAMMER.ChordsSong",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SongId_ChordsSong = c.Decimal(precision: 10, scale: 0),
                        ChordId_ChordsSong = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PROGRAMMER.Chords", t => t.ChordId_ChordsSong)
                .ForeignKey("PROGRAMMER.Songs", t => t.SongId_ChordsSong)
                .Index(t => t.ChordId_ChordsSong)
                .Index(t => t.SongId_ChordsSong);
            
            CreateTable(
                "PROGRAMMER.Chords",
                c => new
                    {
                        ChordsId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 20),
                        Chord = c.Binary(),
                    })
                .PrimaryKey(t => t.ChordsId);
            
            CreateTable(
                "PROGRAMMER.TABsUser",
                c => new
                    {
                        TABsUserId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TABsId_TABsUser = c.Decimal(precision: 10, scale: 0),
                        UserId_TABsUser = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.TABsUserId)
                .ForeignKey("PROGRAMMER.TABs", t => t.TABsId_TABsUser)
                .ForeignKey("PROGRAMMER.User", t => t.UserId_TABsUser)
                .Index(t => t.TABsId_TABsUser)
                .Index(t => t.UserId_TABsUser);
            
            CreateTable(
                "PROGRAMMER.TABs",
                c => new
                    {
                        TABsId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 500),
                        Author = c.String(maxLength: 100),
                        TAB = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TABsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PROGRAMMER.TABsUser", "UserId_TABsUser", "PROGRAMMER.User");
            DropForeignKey("PROGRAMMER.TABsUser", "TABsId_TABsUser", "PROGRAMMER.TABs");
            DropForeignKey("PROGRAMMER.SongsUser", "UserId_SongsUser", "PROGRAMMER.User");
            DropForeignKey("PROGRAMMER.SongsUser", "SongId_SongsUser", "PROGRAMMER.Songs");
            DropForeignKey("PROGRAMMER.ChordsSong", "SongId_ChordsSong", "PROGRAMMER.Songs");
            DropForeignKey("PROGRAMMER.ChordsSong", "ChordId_ChordsSong", "PROGRAMMER.Chords");
            DropForeignKey("PROGRAMMER.NotesUser", "UserId_NotesUser", "PROGRAMMER.User");
            DropForeignKey("PROGRAMMER.NotesUser", "NotesId_NotesUser", "PROGRAMMER.Notes");
            DropForeignKey("PROGRAMMER.ArticlesUser", "UserId_ArticlesUser", "PROGRAMMER.User");
            DropForeignKey("PROGRAMMER.ArticlesUser", "ArticleId_ArticlesUser", "PROGRAMMER.Article");
            DropIndex("PROGRAMMER.TABsUser", new[] { "UserId_TABsUser" });
            DropIndex("PROGRAMMER.TABsUser", new[] { "TABsId_TABsUser" });
            DropIndex("PROGRAMMER.SongsUser", new[] { "UserId_SongsUser" });
            DropIndex("PROGRAMMER.SongsUser", new[] { "SongId_SongsUser" });
            DropIndex("PROGRAMMER.ChordsSong", new[] { "SongId_ChordsSong" });
            DropIndex("PROGRAMMER.ChordsSong", new[] { "ChordId_ChordsSong" });
            DropIndex("PROGRAMMER.NotesUser", new[] { "UserId_NotesUser" });
            DropIndex("PROGRAMMER.NotesUser", new[] { "NotesId_NotesUser" });
            DropIndex("PROGRAMMER.ArticlesUser", new[] { "UserId_ArticlesUser" });
            DropIndex("PROGRAMMER.ArticlesUser", new[] { "ArticleId_ArticlesUser" });
            DropTable("PROGRAMMER.TABs");
            DropTable("PROGRAMMER.TABsUser");
            DropTable("PROGRAMMER.Chords");
            DropTable("PROGRAMMER.ChordsSong");
            DropTable("PROGRAMMER.Songs");
            DropTable("PROGRAMMER.SongsUser");
            DropTable("PROGRAMMER.Notes");
            DropTable("PROGRAMMER.NotesUser");
            DropTable("PROGRAMMER.User");
            DropTable("PROGRAMMER.ArticlesUser");
            DropTable("PROGRAMMER.Article");
        }
    }
}
