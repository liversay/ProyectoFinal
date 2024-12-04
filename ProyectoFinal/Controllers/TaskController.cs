using ProyectoFinal.Data;
using System.Collections.Generic;
using System.Web.Http;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    [Route("task")]
    public class TaskController : ApiController
    {
        private TaskRepository repository = new TaskRepository();

        [HttpGet]
        [Route("task")]
        public IEnumerable<Task> GetAll()
        {
            return repository.GetAllTasks();
        }

        [HttpGet]
        [Route("task/{id}")]
        public Task Get(int id)
        {
            return repository.GetTaskById(id);
        }

        [HttpPost]
        [Route("task")]
        public void Create([FromBody] Task task)
        {
            repository.AddTask(task);
        }

        [HttpPost]
        [Route("task/delete/{id}")]
        public void Delete(int id)
        {
            repository.DeleteTask(id);
        }
    }
}