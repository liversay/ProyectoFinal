using ProyectoFinal.Data;
using System.Collections.Generic;
using System.Web.Http;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class TaskController : ApiController
    {
        private TaskRepository repository = new TaskRepository();

        [HttpGet]
        public IEnumerable<Task> GetAll()
        {
            return repository.GetAllTasks();
        }

        [HttpGet]
        public Task Get(int id)
        {
            return repository.GetTaskById(id);
        }

        [HttpPost]
        public void Create([FromBody] Task task)
        {
            repository.AddTask(task);
        }

        [HttpPut]
        public void Update(int id, [FromBody] Task task)
        {
            task.ID = id;
            repository.UpdateTask(task);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            repository.DeleteTask(id);
        }
    }
}