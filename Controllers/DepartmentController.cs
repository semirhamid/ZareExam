using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZareExam.DTOs;
using ZareExam.Interface;

namespace ZareExam.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentInterface _departmentManager;

        public DepartmentsController(IDepartmentInterface departmentManager)
        {
            _departmentManager = departmentManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentReadDTO>>> GetAllDepartments()
        {
            var departments = await _departmentManager.GetAllDepartmentsAsync();
            if (departments == null)
            {
                return NotFound();
            }
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentReadDTO>> GetDepartmentById(int id)
        {
            var department = await _departmentManager.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentReadDTO>> AddDepartment(DepartmentCreateDTO department)
        {
            try
            {
                var createdDepartment = await _departmentManager.AddDepartmentAsync(department);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentReadDTO>> UpdateDepartment(int id, DepartmentUpdateDTO department)
        {
            try
            {
                var updatedDepartment = await _departmentManager.UpdateDepartmentAsync(id, department);
                if (updatedDepartment == null)
                {
                    return NotFound();
                }
                return Ok(updatedDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteDepartment(int id)
        {
            try
            {
                var deleted = await _departmentManager.DeleteDepartmentAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
