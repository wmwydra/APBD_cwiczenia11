namespace APBD_11.DTOs;

public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    //public int IdPrescription { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}