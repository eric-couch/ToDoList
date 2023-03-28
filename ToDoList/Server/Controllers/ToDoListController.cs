using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Server.Data;
using ToDoList.Server.Data.Migrations;
using ToDoList.Server.Models;
using ToDoList.Shared;

namespace ToDoList.Server.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ToDoListController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Route("api/ToDoList/GetToDoList")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _context.ToDoItems.Where(x => x.ApplicationUserId == user.Id).ToListAsync();
        }

        [HttpGet]
        [Route("api/todoitems/{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            //var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            var todoItem = await _context.ToDoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            if (todoItem.ApplicationUserId != user.Id)
            {
                return Unauthorized();
            }

            return todoItem;
        }
        //api/todoitems/{Id}
        [HttpPut("api/todoitems/{id}")]
        public async Task<IActionResult> PutToDoItem([FromBody] ToDoItem todoItem)
        {
            //if (id != todoItem.Id)
            //{
            //    return BadRequest();
            //}

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (todoItem.ApplicationUserId != user.Id)
            {
                return Unauthorized();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(todoItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }
    }
}
