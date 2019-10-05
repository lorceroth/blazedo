using Blazedo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Blazedo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodosController(TodoContext context) =>
            _context = context;

        [HttpGet]
        public ActionResult<IOrderedEnumerable<Todo>> Index()
        {
            var todos = _context.Todos.OrderBy(t => t.Id).ToList();
            if (!todos.Any())
            {
                return NoContent();
            }

            return Ok(todos);
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> Show(int id)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Todo todo)
        {
            _context.Todos.Add(todo);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Show), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            var existingTodo = _context.Todos.FirstOrDefault(t => t.Id == id);    
            if (existingTodo == null)
            {
                return NotFound();
            }

            // AutoMapper is nice for great things
            existingTodo.Description = todo.Description;
            existingTodo.CompletedAt = todo.CompletedAt;

            _context.Todos.Update(existingTodo);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingTodo = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(existingTodo);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
