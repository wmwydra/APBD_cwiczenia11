using APBD_11.Data;
using APBD_11.DTOs;
using APBD_11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }


    public async Task<PatientWithPerscriptionsDTO> GetPatient(int idPatient)
    {
        var patient = await _context.Patients
            .Where(p => p.IdPatient == idPatient)
            .Select(p => new PatientWithPerscriptionsDTO
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Prescriptions = p.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionWithMedicamentsDTO()
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorDTO
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName
                        },
                        Medicaments = pr.PrescriptionMedicaments
                            .Select(pm => new MedicamentDTO
                            {
                                IdMedicament = pm.Medicament.IdMedicament,
                                Name = pm.Medicament.Name,
                                Dose = pm.Dose,
                                Details = pm.Details
                            })
                            .ToList()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return patient;
    }

    public async Task<Patient> AddPatient(PatientDTO patient, CancellationToken cancellationToken)
    {
        var newPatient = new Patient
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate
        };
        await _context.Patients.AddAsync(newPatient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return newPatient;
    }
    
    public async Task<Prescription?> AddPrescription(AddPrescriptionDTO prescription, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors.FindAsync(prescription.IdDoctor);
        if (doctor == null)
            return null;
        
        var patient = await _context.Patients.FindAsync(prescription.Patient.IdPatient);
        if (patient == null)
        {
            patient = await AddPatient(prescription.Patient, cancellationToken);
        }

        var medicamentIds = prescription.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicamentIds = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync(cancellationToken);

        if (medicamentIds.Except(existingMedicamentIds).Any())
            return null;

        var newPrescription = new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdDoctor = prescription.IdDoctor,
            IdPatient = patient.IdPatient,
            PrescriptionMedicaments = new List<Prescription_Medicament>()
        };

        await _context.Prescriptions.AddAsync(newPrescription, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var m in prescription.Medicaments)
        {
            newPrescription.PrescriptionMedicaments.Add(new Prescription_Medicament
            {
                IdPrescription = newPrescription.IdPrescription,
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return newPrescription;
    }

}