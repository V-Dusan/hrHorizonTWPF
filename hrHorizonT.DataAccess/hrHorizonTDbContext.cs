using hrHorizonT.Model;
using Microsoft.EntityFrameworkCore;

namespace hrHorizonT.DataAccess
{
    public class hrHorizonTDbContext : DbContext
    {
        //private readonly string _connectionString;

        //public hrHorizonTDbContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(_connectionString);
        //}

        public DbSet<Friend> Friends { get; set; }

        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public DbSet<FriendPhoneNumber> FriendPhoneNumbers { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=HorizonT;Username=postgres;Password=2mil479");
    }

}
