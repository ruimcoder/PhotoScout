namespace ScoutterSite.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ScoutterDB : DbContext
    {
        public ScoutterDB()
            : base("name=ScoutterDB")
        {
        }

        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationImage> LocationImages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .Property(e => e.UserId)
                .IsFixedLength();

            modelBuilder.Entity<Location>()
                .Property(e => e.CretedBy)
                .IsFixedLength();

            modelBuilder.Entity<Location>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Location>()
                .Property(e => e.Coordinates)
                .IsFixedLength();

            modelBuilder.Entity<LocationImage>()
                .Property(e => e.ImageTitle)
                .IsFixedLength();

            modelBuilder.Entity<LocationImage>()
                .Property(e => e.FileName)
                .IsFixedLength();

            modelBuilder.Entity<LocationImage>()
                .Property(e => e.OriginalFileName)
                .IsFixedLength();

            modelBuilder.Entity<LocationImage>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<LocationImage>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();
        }
    }
}
