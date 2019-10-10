using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.DTOs;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolTripsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchoolTripsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SchoolTrip>> GetAll()
        {
            List<SchoolTrip> schoolTrips;

            try
            {
                schoolTrips = _unitOfWork.SchoolTripRepository.GetAll();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(schoolTrips);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<SchoolTripDTO>> GetSchoolTripById(long id)
        {
            SchoolTripDTO schoolTrip = _unitOfWork.SchoolTripRepository.GetById(id);

            if (schoolTrip == null)
            {
                return NotFound();
            }

            return Ok(schoolTrip);
        }

        [HttpGet("teacher/{id:int}")]
        public ActionResult<IEnumerable<SchoolTripDTO>> GetAllSchoolTripsByTeacherId(long id)
        {
            List<SchoolTripDTO> schoolTrips;

            try
            {
                schoolTrips = _unitOfWork.SchoolTripRepository.GetAllByTeacherId(id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(schoolTrips);
        }

        [HttpPost]
        public ActionResult<Class> PostSchoolTrip(SchoolTrip schoolTrip)
        {
            _unitOfWork.SchoolTripRepository.Add(schoolTrip);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("GetSchoolTripById", new {id = schoolTrip.Id}, schoolTrip);
        }

        [HttpPut("{id}")]
        public IActionResult PutSchoolTrip(long id, SchoolTrip schoolTrip)
        {
            if (id != schoolTrip.Id)
            {
                return BadRequest();
            }

            if (!_unitOfWork.SchoolTripRepository.SchoolTripExists(id))
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.SchoolTripRepository.Update(schoolTrip);
                _unitOfWork.SaveChanges();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return NoContent();
        }
    }
}