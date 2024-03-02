using HealthTech331.Models;

namespace HealthTech331.Repository
{
    // Repository for managing doctors in the health tech application
    public class DoctorRepository : IRepostoryDoctor
    {
        private readonly HealthTechContext _dbContext;

        // Constructor initializes the database context
        public DoctorRepository()
        {
            _dbContext = new HealthTechContext();
        }

        // Adds a new doctor to the repository
        public Task<Doctor> addDoctor(Doctor @doctor)
        {
            // Get the maximum doctor id in the database
            //var maxId = _dbContext.Set<Doctor>().Max(u => (int?)u.DoctorId);

            // Add the new doctor to the database
            var userEntry = _dbContext.Doctors.Add(doctor);

            // Save changes to the database
            _dbContext.SaveChanges();

            // ResetIdentitySeedForTable(this.GetType().Name, maxId); 

            // Returning null (consider returning the added doctor or a meaningful response)
            return null;

        }

        // Deletes a doctor from the repository
        public void Delete(Doctor @doctor)
        {
            // Get the maximum doctor id in the database
            var maxId = _dbContext.Set<Doctor>().Max(u => (int?)u.DoctorId);

            // Remove the doctor from the database
            _dbContext.Remove(@doctor);

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        // Retrieves all doctors from the repository
        public IEnumerable<Doctor> GetAll()
        {
            // Retrieve all doctors from the database
            var @doctors = _dbContext.Doctors.ToList();
            return @doctors;
        }

        // Retrieves a doctor by its id from the repository
        public async Task<Doctor> GetById(int id)
        {
            // Find the doctor in the database by its id
            var @doctor = _dbContext.Doctors.FirstOrDefault(u => u.DoctorId == id);

            // Return null if the doctor is not found
            if (@doctor == null)
            {
                return null;
            }

            return @doctor;
        }

        // Updates an existing doctor in the repository
        public Doctor Update(Doctor @doctor)
        {
            // Update the doctor in the database
            var doctorEntry = _dbContext.Update(@doctor);

            // Save changes to the database
            _dbContext.SaveChanges();

            // Retrieve the updated doctor from the database
            Doctor u = doctorEntry.Entity;
            return u;
        }
        public IEnumerable<Doctor> GetAllDoctorsBySpecialityId(int specialityID)
        {
            // Retrieve all doctors from the database
            var @doctors = _dbContext.Doctors.Where(c => c.SpecialityId == specialityID).ToList();
            return @doctors;
        }
    }
}
