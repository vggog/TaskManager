using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManager.Models;
using TaskManager.Repositories;
using TaskManager.Schemas;
using TaskManager.Utils;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        ITaskRepository _repository;

        public TasksController(ITaskRepository repository) 
        {
            _repository = repository;
        }

        /// <summary>
        /// Получить все задачи.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
        public IEnumerable<TaskModel> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            TaskModel? task = _repository.GetById(id);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public TaskModel Post([FromBody] TaskCreateSchema taskCreate)
        {
            TaskModel createdTask = new() 
            { 
                Title = taskCreate.title, 
                Description = taskCreate.description, 
                Status = "Открыта" 
            };
            return _repository.Create(createdTask);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public IActionResult Put(int id, [FromBody] TaskUpdateSchema updatedTask)
        {
            if (updatedTask.status != null && !TaskStatusUtil.IsValidTaskStatus(updatedTask.status))
            {
                return BadRequest($"Status field must be: {TaskStatusUtil.GetStatuses(", ")}");
            }

            TaskModel? task = _repository.UpdateTask(id, updatedTask);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
