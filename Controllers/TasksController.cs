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
        /// <summary>
        /// Получить все задачи.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
        public IEnumerable<TaskModel> Get()
        {
            TaskRepository taskRepository = new();
            return taskRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            TaskRepository taskRepository = new();

            TaskModel? task = taskRepository.GetById(id);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public TaskModel Post([FromBody] TaskCreateSchema taskCreate)
        {
            TaskRepository taskRepository = new();

            TaskModel createdTask = new() 
            { 
                Title = taskCreate.title, 
                Description = taskCreate.description, 
                Status = "Открыта" 
            };
            return taskRepository.Create(createdTask);
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

            TaskRepository taskRepository = new();
            TaskModel? task = taskRepository.UpdateTask(id, updatedTask);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Delete(int id)
        {
            TaskRepository taskRepository = new();

            taskRepository.Delete(id);
        }
    }
}
