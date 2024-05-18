using Microsoft.AspNetCore.Mvc;
using System.Collections;
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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskModel>), (int)HttpStatusCode.OK)]
        async public Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        async public Task<IActionResult> Get(int id)
        {
            TaskModel? task = await _repository.GetById(id);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
        async public Task<IActionResult> Post([FromBody] TaskCreateSchema taskCreate)
        {
            TaskModel createdTask = new() 
            { 
                Title = taskCreate.title, 
                Description = taskCreate.description, 
                Status = TaskStatusUtil.GetFirstStatusOfTask()
            };
            return Ok(await _repository.Create(createdTask));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        async public Task<IActionResult> Put(int id, [FromBody] TaskUpdateSchema updatedTask)
        {
            if (updatedTask.status != null && !TaskStatusUtil.IsValidTaskStatus(updatedTask.status))
            {
                return BadRequest($"Status field must be: {TaskStatusUtil.GetStatuses(", ")}");
            }

            TaskModel? task = await _repository.UpdateTask(id, updatedTask);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        async public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
