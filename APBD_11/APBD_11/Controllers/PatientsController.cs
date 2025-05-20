using APBD_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public PatientsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientPrescriptions(int patientId)
        {
            var patient = await _dbService.GetPatient(patientId);
            return Ok(patient);
        }
    }
}

