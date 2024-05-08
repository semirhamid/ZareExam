using System.ComponentModel.DataAnnotations;

namespace ZareExam.DTOs
{
    public class StudentCreateDTO
    {
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

        [Required]
        public int UserId { get; set; }
    }
}
