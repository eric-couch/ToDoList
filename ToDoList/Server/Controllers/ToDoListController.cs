using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Server.Data;
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

    }
}
