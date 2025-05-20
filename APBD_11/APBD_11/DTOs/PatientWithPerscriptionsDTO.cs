using APBD_11.Models;

namespace APBD_11.DTOs;

public class PatientWithPerscriptionsDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
    public ICollection<PrescriptionWithMedicamentsDTO> Prescriptions { get; set; }
}