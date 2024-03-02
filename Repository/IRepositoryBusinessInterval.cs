using HealthTech331.Models;

namespace HealthTech331.Repository
{
    // Interface defining the contract for managing business intervals in the health tech application
    public interface IRepositoryBusinessInterval
    {
        // Retrieves all business intervals from the repository
        IEnumerable<BusinessInterval> GetAll();

        // Retrieves a business interval by its id from the repository
        Task<BusinessInterval> GetById(int id);

        // Updates an existing business interval in the repository
        BusinessInterval Update(BusinessInterval @businessInterval);

        // Deletes a business interval from the repository
        void Delete(BusinessInterval @businessInterval);

        // Adds a new business interval to the repository
        public Task<BusinessInterval> addBusinessInterval(BusinessInterval @businessInterval);
    }
}
