using HealthTech331.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTech331.Repository
{
    // Repository class for managing users in the health tech application
    public class UserRepository : IRepositoryUser
    {
        private readonly HealthTechContext _dbcontext;

        // Constructor initializes the database context
        public UserRepository()
        {
            _dbcontext = new HealthTechContext();
        }

        // Adds a new user to the repository
        public async Task<ApplicationUser> addUser(ApplicationUser @user)
        {
            // Add the new user to the database
            var userEntry = _dbcontext.Add(@user);

            // Save changes to the database
            _dbcontext.SaveChanges();

            // Retrieve the added user from the database
            var u = userEntry.Entity;
            return u;
        }

        // Deletes a user from the repository
        public void Delete(ApplicationUser @user)
        {
            // Remove the user from the database
            var userEntry = _dbcontext.Remove(@user);

            // Save changes to the database
            _dbcontext.SaveChanges();
        }

        // Retrieves all users from the repository
        public IEnumerable<ApplicationUser> GetAll()
        {
            // Retrieve all users from the database
            var users = _dbcontext.ApplicationUsers.ToList();
            return users;
        }

        // Retrieves a user by its id from the repository
        public async Task<ApplicationUser> GetById(int id)
        {
            // Find the user in the database by its id
            var user = _dbcontext.ApplicationUsers.FirstOrDefault(u => u.UserId == id);

            // Return null if the user is not found
            if (user == null)
            {
                return null;
            }

            return user;
        }

        // Updates an existing user in the repository
        public ApplicationUser Update(ApplicationUser @user)
        {
            // Update the user in the database
            var userEntry = _dbcontext.Update(@user);

            // Save changes to the database
            _dbcontext.SaveChanges();

            // Retrieve the updated user from the database
            ApplicationUser u = userEntry.Entity;
            return u;
        }


        // This method updates the points used by a patient identified by the provided ID.
        public void updatePoints(int id, int points)
        {
            // Retrieve the patient entity from the database using the provided ID.
            var patient = _dbcontext.Set<ApplicationUser>().Find(id);
            // Check if the patient's PointsUsed property is null.
            if(patient.PointsUsed == null)
                // If null, initialize it to zero.
                patient.PointsUsed = 0;
            patient.PointsUsed += points;
            // Save changes to the database
            _dbcontext.SaveChanges();
        }
    }
}
