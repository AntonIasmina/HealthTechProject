using HealthTech331.Models;
using HealthTech331.Models.DTO;

namespace HealthTech331.Repository
{
    // Interface defining the contract for managing users in the health tech application
    public interface IRepositoryUser
    {
        // Retrieves all users from the repository
        IEnumerable<ApplicationUser> GetAll();

        // Retrieves a user by its id from the repository
        Task<ApplicationUser> GetById(int id);

        // Updates an existing user in the repository
        ApplicationUser Update(ApplicationUser @user);

        // Deletes a user from the repository
        void Delete(ApplicationUser @user);

        // Adds a new user to the repository
        public Task<ApplicationUser> addUser(ApplicationUser @user);
        void updatePoints(int id, int points);




    }
}
