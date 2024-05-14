using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManager.Models;
using TaskManager.Repositories;

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
    }
}
