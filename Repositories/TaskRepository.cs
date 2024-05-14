using TaskManager.DataBase;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class TaskRepository : BaseRepository<TaskModel>
    {

        public override IEnumerable<TaskModel> GetAll()
        {
            DataBaseContext context = new();
            
            return context.Tasks;
        }

        public override TaskModel Create(TaskModel entity)
        {
            DataBaseContext context = new();

            context.Tasks.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
