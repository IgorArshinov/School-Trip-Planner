using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.DTOs;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeachersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TeacherDTO>> GetTeachers()
        {
            List<TeacherDTO> teachers;
            try
            {
                teachers = _unitOfWork.TeacherRepository.GetAll();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(teachers);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(TeacherDTO teacherDTO)
        {
            var json = JsonConvert.SerializeObject(teacherDTO);
            var teacherDTOFromJson = JsonConvert.DeserializeObject<TeacherDTO>(json);

            var teacher = _unitOfWork.TeacherRepository.Authenticate(teacherDTOFromJson);

            if (teacher == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(new AuthenticationResponse()
            {
                IsAuthenticated = true,
                Teacher = new TeacherDTO()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Surname = teacher.Surname,
                    Username = teacher.Username,
                    LastModified = teacher.LastModified
                }
            });
        }

        [HttpPost("register")]
        public IActionResult Register(TeacherDTO teacherDTO)
        {
            var json = JsonConvert.SerializeObject(teacherDTO);
            var teacherDTOFromJson = JsonConvert.DeserializeObject<TeacherDTO>(json);

            try
            {
                _unitOfWork.TeacherRepository.Add(teacherDTOFromJson);
                _unitOfWork.SaveChanges();
                return Created("GetById", new AuthenticationResponse()
                {
                    IsAuthenticated = true,
                    Teacher = new TeacherDTO()
                    {
                        Id = teacherDTOFromJson.Id,
                        Name = teacherDTOFromJson.Name,
                        Surname = teacherDTOFromJson.Surname,
                        Username = teacherDTOFromJson.Username,
                        LastModified = teacherDTOFromJson.LastModified
                    }
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Teacher> GetTeacher(long id)
        {
            TeacherDTO teacher;
            try
            {
                teacher = _unitOfWork.TeacherRepository.GetById(id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TeacherDTO teacherDTO)
        {
            if (id != teacherDTO.Id)
            {
                return BadRequest();
            }

            if (!_unitOfWork.TeacherRepository.TeacherExists(id))
            {
                return NotFound();
            }

            var json = JsonConvert.SerializeObject(teacherDTO);
            var teacherDTOFromJson = JsonConvert.DeserializeObject<TeacherDTO>(json);

            try
            {
                _unitOfWork.TeacherRepository.Update(teacherDTOFromJson);
                _unitOfWork.SaveChanges();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(new AuthenticationResponse()
            {
                IsAuthenticated = true,
                Teacher = new TeacherDTO()
                {
                    Id = teacherDTOFromJson.Id,
                    Name = teacherDTOFromJson.Name,
                    Surname = teacherDTOFromJson.Surname,
                    Username = teacherDTOFromJson.Username,
                    LastModified = teacherDTOFromJson.LastModified
                }
            });
        }
    }
}