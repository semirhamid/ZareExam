using ZareExam.DTOs;
using ZareExam.Models;

namespace ZareExam.Interface;
    public interface IDepartmentInterface
    {
        Task<IEnumerable<DepartmentReadDTO>> GetAllDepartmentsAsync();
        Task<DepartmentReadDTO> GetDepartmentByIdAsync(int id);
        Task<DepartmentReadDTO> AddDepartmentAsync(DepartmentCreateDTO department);
        Task<DepartmentReadDTO> UpdateDepartmentAsync(int id, DepartmentUpdateDTO department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
