using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToddlersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToddlersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Toddler>> GetToddlers()
        {
            List<Toddler> toddlers;

            try
            {
                toddlers = _unitOfWork.ToddlerRepository.GetAll();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(toddlers);
        }

        [HttpGet("{id}")]
        public ActionResult<Toddler> GetToddler(long id)
        {
            var toddler = _unitOfWork.ToddlerRepository.GetById(id);

            if (toddler == null)
            {
                return NotFound();
            }

            return Ok(toddler);
        }

        [HttpPut("{id}")]
        public IActionResult PutToddler(long id, Toddler toddler)
        {
            if (id != toddler.Id)
            {
                return BadRequest();
            }

            if (!_unitOfWork.ToddlerRepository.ToddlerExists(id))
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.ToddlerRepository.Update(toddler);
                _unitOfWork.SaveChanges();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Toddler> PostToddler(Toddler toddler)
        {
            _unitOfWork.ToddlerRepository.Add(toddler);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("GetToddler", new {id = toddler.Id}, toddler);
        }
    }
}