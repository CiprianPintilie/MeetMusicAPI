using MeetMusic.Provider;
using MeetMusicModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace MeetMusic.Context
{
    public class MeetMusicDbContext : DbContext
    {
        public string ConnectionString { get; set; }


        public MeetMusicDbContext(DbContextOptions<MeetMusicDbContext> options) : base(options)
        {
        }

        /*public MeetMusicDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }*/

        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection(ConnectionString);
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        //{ 
        //    base.OnConfiguring(optionsBuilder); var fact = new LoggerFactory(); 
        //    fact.AddProvider(new SqlLoggerProvider()); 
        //    optionsBuilder.UseLoggerFactory(fact);
        //    optionsBuilder.UseMySQL("server=sylrusfrnrmeetmu.mysql.db;port=3306;database=sylrusfrnrmeetmu;user=sylrusfrnrmeetmu;password=U9bTq6v3");
        //}

        public DbSet<User> Users { get; set; }
    }
}
