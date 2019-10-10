using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Class>> GetClasses()
        {
            List<Class> classes;

            try
            {
                classes = _unitOfWork.ClassRepository.GetAll();
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(classes);
        }
    }
}