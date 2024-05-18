using Microsoft.EntityFrameworkCore;
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

        async public Task<IEnumerable<TaskModel>> GetAll()
        {          
            return await _context.Tasks.ToListAsync();
        }

        async public Task<TaskModel?> GetById(int id)
        {
            try
            {
                return await _context.Tasks.Where(task => task.Id.Equals(id)).FirstAsync();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        async public Task<TaskModel> Create(TaskModel entity)
        {
            await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        async public Task<TaskModel?> UpdateTask(int id, TaskUpdateSchema updatedTask)
        {
            TaskModel? task = await GetById(id);

            if (task == null) return null;

            if (updatedTask.title != null) task.Title = updatedTask.title;
            if (updatedTask.description != null) task.Description = updatedTask.description;
            if (updatedTask.status != null) task.Status = updatedTask.status;

            await _context.SaveChangesAsync();
            return task;
        }

        async public void Delete(int id)
        {
            TaskModel? task = await GetById(id);

            if (task == null) return;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
