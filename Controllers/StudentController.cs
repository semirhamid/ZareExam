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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentInterface _studentManager;

        public StudentsController(IStudentInterface studentManager)
        {
            _studentManager = studentManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDTO>>> GetAllStudents()
        {
            var students = await _studentManager.GetAllStudentsAsync();
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReadDTO>> GetStudentById(int id)
        {
            var student = await _studentManager.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentReadDTO>> AddStudent(StudentCreateDTO student)
        {
            try
            {
                var studentExist = await _studentManager.GetStudentByEmailAsync(student.Email);
                if (studentExist != null)
                {
                    return BadRequest("Student already exist");
                }
                var createdStudent = await _studentManager.AddStudentAsync(student);
                if (createdStudent == null)
                {
                    return BadRequest("Failed to create student");
                }
                return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentReadDTO>> UpdateStudent(int id, StudentUpdateDTO student)
        {
            var updatedStudent = await _studentManager.UpdateStudentAsync(id, student);
            if (updatedStudent == null)
            {
                return NotFound();
            }
            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            var deleted = await _studentManager.DeleteStudentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(deleted);
        }
    }
}
