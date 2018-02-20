using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace MeetMusic.Data
{
    public class MeetDbContext : DbContext
    {
        public string ConnectionString { get; set; }

        public MeetDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
