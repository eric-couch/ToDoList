using Microsoft.AspNetCore.Identity;
using ToDoList.Shared;

namespace ToDoList.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}