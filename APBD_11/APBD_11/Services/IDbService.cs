using APBD_11.DTOs;
using APBD_11.Models;

namespace APBD_11.Services;

public interface IDbService
{
    Task<PatientWithPerscriptionsDTO> GetPatient(int idPatient);
    
    Task<Patient> AddPatient(PatientDTO patient, CancellationToken cancellationToken);
    
    Task<Prescription?> AddPrescription(AddPrescriptionDTO prescription, CancellationToken cancellationToken);
}