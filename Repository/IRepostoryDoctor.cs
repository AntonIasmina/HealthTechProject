using HealthTech331.Models;

namespace HealthTech331.Repository
{
    // Interface defining the contract for managing doctors in the health tech application
    public interface IRepostoryDoctor
    {
        // Retrieves all doctors from the repository
        IEnumerable<Doctor> GetAll();

        // Retrieves a doctor by its id from the repository
        Task<Doctor> GetById(int id);

        // Updates an existing doctor in the repository
        Doctor Update(Doctor doctor);

        // Deletes a doctor from the repository
        void Delete(Doctor @doctor);

        // Adds a new doctor to the repository
        public Task<Doctor> addDoctor(Doctor @doctor);

        IEnumerable<Doctor> GetAllDoctorsBySpecialityId(int specialityID);

    }
}
