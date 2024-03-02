using HealthTech331.Models;

namespace HealthTech331.Repository
{
    // Repository class for managing specialities in the health tech application
    public class SpecialityRepository : IRepositorySpeciality
    {
        private readonly HealthTechContext _dbContext;

        // Constructor initializes the database context
        public SpecialityRepository()
        {
            _dbContext = new HealthTechContext();
        }

        // Adds a new speciality to the repository
        public Task<Speciality> addSpeciality(Speciality speciality)
        {
            // Get the maximum speciality id in the database
            var maxId = _dbContext.Set<Speciality>().Max(u => (int?)u.SpecialityId);

            // Add the new speciality to the database
            var userEntry = _dbContext.Specialities.Add(speciality);

            // Save changes to the database
            _dbContext.SaveChanges();

            // ResetIdentitySeedForTable(this.GetType().Name, maxId);

            return null;
        }

        // Deletes a speciality from the repository
        public void Delete(Speciality speciality)
        {
            // Get the maximum speciality id in the database
            var maxId = _dbContext.Set<Speciality>().Max(u => (int?)u.SpecialityId);

            // Remove the speciality from the database
            _dbContext.Remove(speciality);

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        // Retrieves all specialities from the repository
        public IEnumerable<Speciality> GetAll()
        {
            // Retrieve all specialities from the database
            var specialities = _dbContext.Specialities.ToList();
            return specialities;
        }

        // Retrieves a speciality by its id from the repository
        public async Task<Speciality> GetById(int id)
        {
            // Find the speciality in the database by its id
            var speciality = _dbContext.Specialities.FirstOrDefault(u => u.SpecialityId == id);

            // Return null if the speciality is not found
            if (speciality == null)
            {
                return null;
            }

            return speciality;
        }

        // Updates an existing speciality in the repository
        public Speciality Update(Speciality speciality)
        {
            // Update the speciality in the database
            var specialityEntry = _dbContext.Update(speciality);

            // Save changes to the database
            _dbContext.SaveChanges();

            // Retrieve the updated speciality from the database
            Speciality u = specialityEntry.Entity;
            return u;
        }
    }
}
