using APBD_11.DTOs;
using APBD_11.Models;
using APBD_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public PrescriptionsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> PostNewPrescription([FromBody] AddPrescriptionDTO prescription, CancellationToken ct)
        {
            if (prescription.DueDate < prescription.Date)
                return BadRequest("DueDate must be greater than or equal to Date");
            if (prescription.Medicaments.Count > 10)
                return BadRequest("Prescription cannot include more than 10 medicaments.");

            var result = await _dbService.AddPrescription(prescription, ct);
            if (result == null)
                return BadRequest("Invalid input data.");

            return Created($"/api/prescriptions/{result.IdPrescription}", result);
        }
    }
}

