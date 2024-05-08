using System.ComponentModel.DataAnnotations;

namespace ZareExam.DTOs
{
    public class DepartmentReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentReadDTO> Students { get; set; }
    }
}
