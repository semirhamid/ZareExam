using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZareExam.DTOs;
using ZareExam.Infrastructure.Db;
using ZareExam.Interface;
using ZareExam.Models;
using ZareExam.Models.Entity;

namespace ZareExam.BusinessLogic;
public class StudentManager : IStudentInterface
{
  
    private readonly AuthDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentManager> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IDepartmentInterface _departmentManager;
      public StudentManager( AuthDbContext context, IMapper mapper, ILogger<StudentManager> logger, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IDepartmentInterface departmentManager)
      {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _departmentManager = departmentManager;
      }

    public async Task<StudentReadDTO> AddStudentAsync(StudentCreateDTO student)
    {
        var user = await _userManager.FindByEmailAsync(student.Email);
        if (user == null)
        {
            return null;
        }
        var department = await _context.Department.FirstOrDefaultAsync(x => x.Id == student.DepartmentId);
        if(department == null)
        {
            return null;
        }
        var studentEntity = _mapper.Map<Student>(student);
        studentEntity.UserId = user.Id;
        studentEntity.User = user;
        studentEntity.DepartmentId = student.DepartmentId;
        studentEntity.Department = department;
        _context.Students.Add(studentEntity);

        _context.SaveChanges();
        var role = _roleManager.FindByNameAsync("user").Result;
        if (role != null)
        {
            try{
              var removeUserRoleResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (removeUserRoleResult.Succeeded)
            {
          var studentRole = _roleManager.FindByNameAsync("student").Result;
          if (studentRole != null)
          {
              await _userManager.AddToRoleAsync(user, studentRole.Name);
          }
          else
          {
              throw new Exception("Student role not found");
          }
            }
            else
            {
                throw new Exception("User role not found");
            }
            }catch(Exception e){
              _logger.LogError(e, e.Message);
            }
        }
        else
        {
            throw new Exception("Role not found");
        }
        return await Task.FromResult(_mapper.Map<StudentReadDTO>(studentEntity));
    }


        public async Task<bool> DeleteStudentAsync(int id)
        {
          try
          {
            var studentEntity = await _context.Students.FindAsync(id);
            if (studentEntity == null)
            {
              return false;
            }

            _context.Students.Remove(studentEntity);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(studentEntity.UserId);
            if (user != null)
            {
              var studentRole = await _roleManager.FindByNameAsync("student");
              if (studentRole != null)
              {
                await _userManager.RemoveFromRoleAsync(user, studentRole.Name);
              }
              else
              {
                throw new Exception("Student role not found");
              }
            }
            else
            {
              throw new Exception("User not found");
            }

            return true;
          }
          catch (Exception ex)
          {
            _logger.LogError(ex, ex.Message);
            return false;
          }
        }
    

    public async Task<IEnumerable<StudentReadDTO>> GetAllStudentsAsync()
    {
        try
        {
            var students = await _context.Students.ToListAsync();
            return _mapper.Map<IEnumerable<StudentReadDTO>>(students);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<StudentReadDTO> GetStudentByEmailAsync(string email)
    {
        var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
        return _mapper.Map<StudentReadDTO>(student);

    }

    public async Task<StudentReadDTO> GetStudentByIdAsync(int id)
    {
        try
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<StudentReadDTO>(student);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<StudentReadDTO> UpdateStudentAsync(int id, StudentUpdateDTO student)
    {
        try
        {
            var studentEntity = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (studentEntity == null)
            {
                return null;
            }
            studentEntity.Name = student.Name;
            studentEntity.Email = student.Email;
            studentEntity.StudentId = student.StudentId;
            studentEntity.BirthDate = student.BirthDate;
            studentEntity.DepartmentId = student.DepartmentId;
            _context.SaveChanges();
            return _mapper.Map<StudentReadDTO>(studentEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }
    
}
