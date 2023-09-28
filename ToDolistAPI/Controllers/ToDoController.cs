using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDolistAPI.DbContext;
using ToDolistAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ToDolistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ToDoItemExists(id))
                {
                    throw;
                    
                }
                else
                {
                    return NotFound(); 
                }
            }



            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTodoItem(int id, [FromBody] JsonPatchDocument<ToDoItem> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(todoItem, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
