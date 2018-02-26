using MeetMusic.Provider;
using MeetMusicModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MeetMusic.Context
{
    public class MeetMusicDbContext : DbContext
    {
        public string ConnectionString { get; set; }


        public MeetMusicDbContext(DbContextOptions<MeetMusicDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); var fact = new LoggerFactory();
            fact.AddProvider(new SqlLoggerProvider());
            optionsBuilder.UseLoggerFactory(fact);
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
