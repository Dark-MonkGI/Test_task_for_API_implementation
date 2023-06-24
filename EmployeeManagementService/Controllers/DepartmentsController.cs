using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementService.Models;

namespace EmployeeManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// Контроллер для обработки всех запросов, связанных с подразделениями (добавление нового подразделения,
        /// удаление подразделения, получение информации о подразделении, получение списка всех подразделений).
        /// </summary>
        

        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// GET: api/Department
        /// Получить список всех подразделений
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.Include(d => d.Employees).ToListAsync();
        }

        /// <summary>
        /// GET: api/Department/5
        /// Получить информацию о конкретном подразделении со списком сотрудников
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)        
                return NotFound();
            
            return department;
        }


        /// <summary>
        /// POST: api/Department
        /// Добавление нового подразделения
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        }


        /// <summary>
        /// DELETE: api/Department/5
        /// Удаление подразделения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)         
                return NotFound();
            
            // проверка на наличие активных заданий у сотрудников данного подразделения
            var hasActiveTasks = _context.EmployeeTasks
                .Include(et => et.Employee)
                .Where(et => et.Employee.DepartmentId == id && !et.IsTaskCompleted)
                .Any();

            if (hasActiveTasks)           
                return BadRequest("Department has employees with active tasks.");
            
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
