using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskManager.Domain.Identities;

namespace TaskManager.Application.DTOs
{
    public class AddTaskDto
    {
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }
        public string? Description { get; set; }

        public string UserId { get; set; }
    }
}
