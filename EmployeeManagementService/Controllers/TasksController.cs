using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementService.Models;
using Task = EmployeeManagementService.Models.Task;

namespace EmployeeManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// Контроллер для обработки всех запросов, связанных с заданиями (добавление нового задания,
        /// удаление задания, получение информации о конкретном задании, завершение задания сотрудником).
        /// </summary>



        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/Task
        /// Получить список всех заданий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        /// <summary>
        /// GET: api/Task/5
        /// Получить информацию о конкретном задании
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        /// <summary>
        /// POST: api/Task
        /// Добавление нового задания
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Task>> AddTask(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        /// <summary>
        /// DELETE: api/Task/5
        /// Удаление задания
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)          
                return NotFound();
            
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// POST: api/Task/CompleteTask
        /// Завершение задания конкретным сотрудником
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost("CompleteTask")]
        public async Task<ActionResult> CompleteTask(int employeeId, int taskId)
        {
            var employeeTask = await _context.EmployeeTasks
                .FirstOrDefaultAsync(et => et.EmployeeId == employeeId && et.TaskId == taskId);

            if (employeeTask == null)            
                return NotFound();
            
            employeeTask.IsTaskCompleted = true;

            // Проверка, все ли сотрудники выполнили данное задание. Если да, то задание считается выполненным.
            var allTaskCompleted = !_context.EmployeeTasks.Any(et => et.TaskId == taskId && !et.IsTaskCompleted);
            if (allTaskCompleted)
            {
                var task = await _context.Tasks.FindAsync(taskId);
                task.IsCompleted = true;
                task.CompletionDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
