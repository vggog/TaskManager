using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.DataBase
{
    public class DataBaseContext : DbContext
    {
        SQLiteDBSettings dBSettings;

        public DbSet<TaskModel> Tasks => Set<TaskModel>();

        public DataBaseContext(IConfiguration conf)
        {
            dBSettings = conf.GetSection(SQLiteDBSettings.DBSettings).Get<SQLiteDBSettings>();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(string.Format("Data Source={0}", dBSettings.SQLiteConnectionString));
        }
    }
}
