using hrHorizonT.Model;
using Microsoft.EntityFrameworkCore;

namespace hrHorizonT.DataAccess
{
    public class hrHorizonTDbContext : DbContext
    {       

        public DbSet<Friend> Friends { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<FriendPhoneNumber> FriendPhoneNumbers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Drzava> Drzavas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=HorizonT;Username=postgres;Password=2mil479");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friend>().UseXminAsConcurrencyToken();
            modelBuilder.Entity<Drzava>().UseXminAsConcurrencyToken();

            modelBuilder.Entity<Drzava>().HasIndex(d => new { d.Sifra}).IsUnique();
            modelBuilder.Entity<Drzava>().HasIndex(d => new { d.Oznaka}).IsUnique();
            modelBuilder.Entity<Drzava>().HasIndex(d => new { d.Naziv }).IsUnique();

        }
    }



}
