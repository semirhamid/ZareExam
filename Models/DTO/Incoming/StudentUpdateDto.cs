using System;
using System.ComponentModel.DataAnnotations;

namespace ZareExam.DTOs
{
    public class StudentUpdateDTO
    {
        [StringLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string StudentId { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public int DepartmentId { get; set; }

        public string UserId { get; set; }
    }
}
