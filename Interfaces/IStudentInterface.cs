using ZareExam.DTOs;
using ZareExam.Models;

namespace ZareExam.Interface;
    public interface IStudentInterface
    {
        Task<IEnumerable<StudentReadDTO>> GetAllStudentsAsync();
        Task<StudentReadDTO> GetStudentByIdAsync(int id);
        Task<StudentReadDTO> GetStudentByEmailAsync(string email);
        Task<StudentReadDTO> AddStudentAsync(StudentCreateDTO student);
        Task<StudentReadDTO> UpdateStudentAsync(int id, StudentUpdateDTO student);
        Task<bool> DeleteStudentAsync(int id);
    }
