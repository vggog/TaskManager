using TaskManager.DataBase;
using TaskManager.Models;
using TaskManager.Schemas;

namespace TaskManager.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        DataBaseContext _context;

        public TaskRepository(DataBaseContext context) 
        {
            _context = context;
        }

        public IEnumerable<TaskModel> GetAll()
        {          
            return _context.Tasks;
        }

        public TaskModel? GetById(int id)
        {
            try
            {
                return _context.Tasks.Where(task => task.Id.Equals(id)).First();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public TaskModel Create(TaskModel entity)
        {
            _context.Tasks.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TaskModel? UpdateTask(int id, TaskUpdateSchema updatedTask)
        {
            TaskModel? task = GetById(id);

            if (task == null) return null;

            if (updatedTask.title != null) task.Title = updatedTask.title;
            if (updatedTask.description != null) task.Description = updatedTask.description;
            if (updatedTask != null) task.Status = updatedTask.status;

            _context.Tasks.Update(task);
            _context.SaveChanges();
            return task;
        }

        public void Delete(int id)
        {
            TaskModel? task = GetById(id);

            if (task == null) return;

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
