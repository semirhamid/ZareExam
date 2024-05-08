using System.ComponentModel.DataAnnotations;

namespace ZareExam.DTOs
{
    public class DepartmentCreateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be between {2} and {1} characters long", MinimumLength = 1)]
        public string Name { get; set; }
    }
}
