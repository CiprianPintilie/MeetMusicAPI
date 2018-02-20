using System;
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


        public MeetMusicDbContext(DbContextOptions<MeetMusicDbContext> options) :base(options)
        {}

        /*public MeetMusicDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }*/

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            base.OnConfiguring(optionsBuilder); var fact = new LoggerFactory(); 
            fact.AddProvider(new SQLLoggerProvider()); 
            optionsBuilder.UseLoggerFactory(fact);
        }

        public DbSet<User> Users { get; set; }
    }
}
