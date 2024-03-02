using HealthTech331.Models;

namespace HealthTech331.Repository
{
    // Interface defining the contract for managing appointments in the health tech application
    public interface IRepositoryAppointment
    {
        // Retrieves all appointments from the repository
        IEnumerable<Appointment> GetAll();

        // Retrieves an appointment by its id from the repository
        Task<Appointment> GetById(int id);

        // Updates an existing appointment in the repository
        Appointment Update(Appointment @appointment);

        // Deletes an appointment from the repository
        void Delete(int id);

        // Adds a new appointment to the repository
        Task<Appointment> addAppointment(Appointment @appointment);

        // Find all appointments for a user based on user ID
        Task<IEnumerable<Appointment>> findAllAppointmentsByUserId(int id);

        // Find all appointments for a doctor based on doctor ID
        Task<IEnumerable<Appointment>> findAllAppointmentsByDoctorId(int id);

        // Find upcoming appointments for a user based on user ID
        Task<IEnumerable<Appointment>> findUpcomingAppointmentsByUserId(int id);

        // Find upcoming appointments for a doctor based on doctor ID
        Task<IEnumerable<Appointment>> findUpcomingAppointmentsByDoctorId(int id);

        // Find available time spans for appointments on a specific date for a doctor based on doctor ID
        Task<List<TimeSpan>> findAvailableTimeSpanForAppointmentByDateAndDoctorId(int id, DateTime date);
        // Asynchronously finds and counts all past appointments associated with a specific user ID.
        Task<int> findAllPastAppointmentsByUserId(int id);
    }
}
