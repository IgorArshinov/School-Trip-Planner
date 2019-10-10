using System;
using Microsoft.AspNetCore.Mvc;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanToddlersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScanToddlersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPut("{id}")]
        public IActionResult PutScanToddler(long id, ScanToddler scanToddler)
        {
            if (id != scanToddler.ToddlerId)
            {
                return BadRequest();
            }

            if (!_unitOfWork.ScanToddlerRepository.ScanToddlerExists(id))
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.ScanToddlerRepository.Update(scanToddler);
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