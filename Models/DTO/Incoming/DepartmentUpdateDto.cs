using System.ComponentModel.DataAnnotations;

namespace ZareExam.DTOs
{
    public class DepartmentUpdateDTO
    {
      public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
