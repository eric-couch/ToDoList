using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Shared
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public int ToDoItemId { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
