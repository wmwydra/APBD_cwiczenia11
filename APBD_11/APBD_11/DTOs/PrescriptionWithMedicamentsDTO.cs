using APBD_11.Models;

namespace APBD_11.DTOs;

public class PrescriptionWithMedicamentsDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    //public int IdPatient { get; set; }
    
    public ICollection<MedicamentDTO> Medicaments { get; set; }    
    
    public DoctorDTO Doctor { get; set; }
}