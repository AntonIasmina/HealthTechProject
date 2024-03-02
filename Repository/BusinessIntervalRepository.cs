using HealthTech331.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTech331.Repository
{
    // Repository for managing business intervals in the health tech application
    public class BusinessIntervalRepository : IRepositoryBusinessInterval
    {
        private readonly HealthTechContext _dbContext;

        // Constructor initializes the database context
        public BusinessIntervalRepository()
        {
            _dbContext = new HealthTechContext();
        }

        // Adds a new business interval to the repository
        public Task<BusinessInterval> addBusinessInterval(BusinessInterval @businessInterval)
        {
            
            // Get the maximum business interval id in the database
            var maxId = _dbContext.Set<BusinessInterval>().Max(u => (int?)u.BusinessIntervalId);

            // Add the new business interval to the database
            var userEntry = _dbContext.BusinessIntervals.Add(@businessInterval);

            // Save changes to the database
            _dbContext.SaveChanges();

            // ResetIdentitySeedForTable(this.GetType().Name, maxId); 

            return null;

        }

        // Deletes a business interval from the repository
        public void Delete(BusinessInterval @businessInterval)
        {
            // Get the maximum business interval id in the database
            var maxId = _dbContext.Set<BusinessInterval>().Max(u => (int?)u.BusinessIntervalId);

            // Remove the business interval from the database
            _dbContext.Remove(businessInterval);

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        // Retrieves all business intervals from the repository
        public IEnumerable<BusinessInterval> GetAll()
        {
            // Retrieve all business intervals from the database
            var @businessIntervals = _dbContext.BusinessIntervals.ToList();
            return @businessIntervals;
        }

        // Retrieves a business interval by its id from the repository
        public async Task<BusinessInterval> GetById(int id)
        {
            var businessInterval = _dbContext.BusinessIntervals.Include(bi => bi.Doctors).FirstOrDefault(bi => bi.Doctors.Any(d => d.DoctorId == id));
            // Return null if the business interval is not found
            if (businessInterval == null)
            {
                return null;
            }
           
            return @businessInterval;
        }

        // Updates an existing business interval in the repository
        public BusinessInterval Update(BusinessInterval @businessInterval)
        {
            // Update the business interval in the database
            var businessIntervalEntry = _dbContext.Update(@businessInterval);

            // Save changes to the database
            _dbContext.SaveChanges();

            // Retrieve the updated business interval from the database
            BusinessInterval u = businessIntervalEntry.Entity;
            return u;
        }
    }
}
