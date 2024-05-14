using TaskManager.DataBase;
using TaskManager.Models;
using TaskManager.Schemas;

namespace TaskManager.Repositories
{
    public class TaskRepository : BaseRepository<TaskModel>
    {

        public override IEnumerable<TaskModel> GetAll()
        {
            DataBaseContext context = new();
            
            return context.Tasks;
        }

        public override TaskModel? GetById(int id)
        {
            DataBaseContext context = new();

            try
            {
                return context.Tasks.Where(task => task.Id.Equals(id)).First();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public override TaskModel Create(TaskModel entity)
        {
            DataBaseContext context = new();

            context.Tasks.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public TaskModel? UpdateTask(int id, TaskUpdateSchema updatedTask)
        {
            DataBaseContext context = new();

            TaskModel? task = GetById(id);

            if (task == null) return null;

            if (updatedTask.title != null) task.Title = updatedTask.title;
            if (updatedTask.description != null) task.Description = updatedTask.description;
            if (updatedTask != null) task.Status = updatedTask.status;

            context.Tasks.Update(task);
            context.SaveChanges();
            return task;
        }
    }
}
