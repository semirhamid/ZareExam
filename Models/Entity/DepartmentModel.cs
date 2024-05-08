using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZareExam.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
