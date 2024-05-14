using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<TaskModel> Tasks => Set<TaskModel>();

        public DataBaseContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=tasks.db");
        }
    }
}
