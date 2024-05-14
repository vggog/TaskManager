using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TaskManager.DataBase;
using TaskManager.Models;
using TaskManager.Repositories;
using TaskManager.Schemas;

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
    }
}
