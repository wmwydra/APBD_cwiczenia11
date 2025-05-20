using System.Data.Common;
using System.Runtime.InteropServices.JavaScript;
using APBD_11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "James", LastName = "Bond", Email = "james@gmail.com" },
            new Doctor() { IdDoctor = 2, FirstName = "Alice", LastName = "Cooper", Email = "alice@gmail.com" },
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient()
            {
                IdPatient = 1, FirstName = "David", LastName = "Bowie",
                BirthDate = new DateTime(1965, 3, 12)
            },
            new Patient()
            {
                IdPatient = 2, FirstName = "Celine", LastName = "Dion",
                BirthDate = new DateTime(1971, 6, 1)
            }
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() { IdMedicament = 6, Description = "painkiller", Name = "Apap", Type = "pills"},
            new Medicament() { IdMedicament = 2, Description = "cough medicine", Name = "Herbapect", Type = "syroup" }
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription()
            {
                IdPrescription = 10, Date = new DateTime(2018, 3, 9),
                DueDate = new DateTime(2025, 6, 15), IdPatient = 1, IdDoctor = 2
            }
        });

        modelBuilder.Entity<Prescription_Medicament>().HasData(new List<Prescription_Medicament>()
        {
            new Prescription_Medicament()
            {
                IdMedicament = 6, IdPrescription = 10,
                Details = "Patient has a headache", Dose = 2
            }
        });
    }
}