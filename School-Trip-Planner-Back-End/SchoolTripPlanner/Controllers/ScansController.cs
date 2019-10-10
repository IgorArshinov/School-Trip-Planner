using Microsoft.AspNetCore.Mvc;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScansController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScansController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult<Scan> PostScan(Scan scan)
        {
            _unitOfWork.ScanRepository.Add(scan);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("GetScanById", new {id = scan.Id}, scan);
        }

        [HttpGet("{id}")]
        public ActionResult<Scan> GetScanById(long id)
        {
            var scan = _unitOfWork.ScanRepository.GetById(id);

            if (scan == null)
            {
                return NotFound();
            }

            return Ok(scan);
        }
    }
}