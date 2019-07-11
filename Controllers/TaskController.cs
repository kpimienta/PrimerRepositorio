using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EjemploNetAngular.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EjemploNetAngular.Models {
    [Route ("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase {
        private readonly TaskContext _context;
        public TaskController (TaskContext context) {
            _context = context;
            if (_context.TaskItems.Count () == 0) {
                // Crea un nuevo item si la coleccion esta vacia,
                // lo que significa que no puedes borrar todos los Items.
                _context.TaskItems.Add (new TaskItem { Title = "Priorizar el proyecto", Description = "Priorizar", Priority = true });
                _context.TaskItems.Add (new TaskItem {  Title = "Calendario el proyecto", Description = "Priorizar", Priority = true });
                _context.SaveChanges ();
            }
        }
        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems () {
            return await _context.TaskItems.ToListAsync ();
        }
        // GET: api/Task/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem (int id) {
            var taskItem = await _context.TaskItems.FindAsync (id);
            if (taskItem == null) {
                return NotFound ();
            }
            return taskItem;
        }

        // PUT: api/Task/5
        [HttpPut ("{id}")]
        public async Task<IActionResult> PutTaskItem (int id, TaskItem item) {
            if (id != item.Id) {
                return BadRequest ();
            }
            _context.Entry (item).State = EntityState.Modified;
            await _context.SaveChangesAsync ();
            return NoContent ();
        }

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem (TaskItem item) {
            _context.TaskItems.Add (item);
            await _context.SaveChangesAsync ();
            return CreatedAtAction (nameof (GetTaskItem), new { id = item.Id }, item);
        }

        // DELETE: api/Todo/5
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteTaskItem (int id) {
            var TaskItem = await
            _context.TaskItems.FindAsync (id);
            if (TaskItem == null) {
                return NotFound ();
            }

            _context.TaskItems.Remove (TaskItem);
            await _context.SaveChangesAsync ();
            return NoContent ();
        }
    }
}