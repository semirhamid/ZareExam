using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZareExam.DTOs;
using ZareExam.Infrastructure.Db;
using ZareExam.Interface;
using ZareExam.Models;
using ZareExam.Models.Entity;

namespace ZareExam.BusinessLogic;
public class DepartmentManager : IDepartmentInterface
{
  
    private readonly AuthDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<DepartmentManager> _logger;
      public DepartmentManager( AuthDbContext context, IMapper mapper, ILogger<DepartmentManager> logger)
      {
        _context = context;
        _mapper = mapper;
        _logger = logger;
      }

public async Task<DepartmentReadDTO> AddDepartmentAsync(DepartmentCreateDTO department)
{
    try
    {
        var departmentEntity = _mapper.Map<Department>(department);
        _context.Department.Add(departmentEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<DepartmentReadDTO>(departmentEntity);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, ex.Message);
        throw; 
    }
}

public async Task<bool> DeleteDepartmentAsync(int id)
{
    try
    {
        var department = await _context.Department.FindAsync(id);
        if (department == null)
        {
            return false;
        }
        _context.Department.Remove(department);
        await _context.SaveChangesAsync();
        return true;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, ex.Message);
        throw; 
    }
}

public async Task<IEnumerable<DepartmentReadDTO>> GetAllDepartmentsAsync()
{
    try
    {
        var departments = await _context.Department.Include(x =>x.Students).ToListAsync();

        return _mapper.Map<IEnumerable<DepartmentReadDTO>>(departments);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, ex.Message);
        throw; 
    }
}

public async Task<DepartmentReadDTO> GetDepartmentByIdAsync(int id)
{
    try
    {
        var department = await _context.Department.Include(x => x.Students).FirstOrDefaultAsync(x=> x.Id == id);
        if (department == null)
        {
            return null;
        }
        return _mapper.Map<DepartmentReadDTO>(department);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, ex.Message);
        throw; 
    }
}

public async Task<DepartmentReadDTO> UpdateDepartmentAsync(int id, DepartmentUpdateDTO department)
{
    try
    {
        var departmentEntity = await _context.Department.FindAsync(id);
        if (departmentEntity == null)
        {
            return null;
        }
        departmentEntity.Name = department.Name;
        await _context.SaveChangesAsync();
        return _mapper.Map<DepartmentReadDTO>(departmentEntity);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, ex.Message);
        throw; 
    }
}

}