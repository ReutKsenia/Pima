using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    class OracleDbContext : DbContext
    {
        public OracleDbContext() : base("OracleDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PROGRAMMER");
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Chords> Chords { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<Songs> Songs { get; set; }
        public virtual DbSet<TABs> TABs { get; set; }
        public virtual DbSet<ChordsSong> ChordsSongs { get; set; }
        public virtual DbSet<SongsUser> SongsUsers { get; set; }
        public virtual DbSet<NotesUser> NotesUsers { get; set; }
        public virtual DbSet<TABsUser> TABsUsers { get; set; }
        public virtual DbSet<ArticlesUser> ArticlesUsers { get; set; }
    }
}
