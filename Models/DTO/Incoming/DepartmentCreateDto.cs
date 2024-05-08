using System.ComponentModel.DataAnnotations;

namespace ZareExam.DTOs
{
    public class DepartmentCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
