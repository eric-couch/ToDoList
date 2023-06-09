﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Shared
{
    public class ToDoItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Title.")]
        [StringLength(50, ErrorMessage = "Title must be less than 50 characters.")]
        public string Title { get; set; }
        public ToDoType Type { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }
        public string ApplicationUserId { get; set; }

        public List<ChecklistItem>? Checklist { get; set; }
        public string? Notes { get; set; }
    }
}
