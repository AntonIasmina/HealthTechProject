using HealthTech331.Models;

namespace HealthTech331.Repository
{
    // Interface defining the contract for managing specialities in the health tech application
    public interface IRepositorySpeciality
    {
        // Retrieves all specialities from the repository
        IEnumerable<Speciality> GetAll();

        // Retrieves a speciality by its id from the repository
        Task<Speciality> GetById(int id);

        // Updates an existing speciality in the repository
        Speciality Update(Speciality @speciality);

        // Deletes a speciality from the repository
        void Delete(Speciality @speciality);

        // Adds a new speciality to the repository
        public Task<Speciality> addSpeciality(Speciality @speciality);
    }
}
