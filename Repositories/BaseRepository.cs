using TaskManager.Models;

namespace TaskManager.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        public abstract IEnumerable<T> GetAll();
        public abstract TaskModel GetById(int id);
        public abstract T Create(T entity);
        public abstract void Delete(int id);
    }
}
