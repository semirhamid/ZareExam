using System.ComponentModel.DataAnnotations;

namespace ZareExam.DTOs
{

    public class StudentReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string StudentId { get; set; }
        public DateTime BirthDate { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
    }
}
