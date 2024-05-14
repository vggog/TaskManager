using TaskManager.Models;
using TaskManager.Schemas;

namespace TaskManager.Repositories
{
    public interface ITaskRepository
    {
        public IEnumerable<TaskModel> GetAll();
        public TaskModel GetById(int id);
        public TaskModel Create(TaskModel entity);
        public TaskModel? UpdateTask(int id, TaskUpdateSchema updatedTask);
        public void Delete(int id);
    }
}
