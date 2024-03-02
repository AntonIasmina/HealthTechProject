using HealthTech331.Models;

namespace HealthTech331.Repository
{
    // Interface defining the contract for managing patients in the health tech application
    public interface IRepositoryPatient
    {
        // Retrieves all patients from the repository
        IEnumerable<Patient> GetAllPatients();

        // Retrieves a patient by its id from the repository
        Patient GetPatientById(int id);

        // Retrieves a patient by their CNP (personal identification number) from the repository
        Patient GetPatientByCNP(string CNP);
    }
}
