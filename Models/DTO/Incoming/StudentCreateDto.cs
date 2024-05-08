using System;
using System.ComponentModel.DataAnnotations;

namespace ZareExam.DTOs
{
    public class StudentCreateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be between {2} and {1} characters long", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [StringLength(50, ErrorMessage = "Student ID must be between {2} and {1} characters long", MinimumLength = 1)]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive integer")]
        public int DepartmentId { get; set; }
    }
}

