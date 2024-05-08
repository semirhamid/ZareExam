using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZareExam.Models.Entity;

namespace ZareExam.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }

}
