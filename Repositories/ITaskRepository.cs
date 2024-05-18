using TaskManager.Models;
using TaskManager.Schemas;

namespace TaskManager.Repositories
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskModel>> GetAll();
        public Task<TaskModel> GetById(int id);
        public Task<TaskModel> Create(TaskModel entity);
        public Task<TaskModel?> UpdateTask(int id, TaskUpdateSchema updatedTask);
        public void Delete(int id);
    }
}
