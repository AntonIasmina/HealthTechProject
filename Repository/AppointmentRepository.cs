using HealthTech331.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HealthTech331.Repository
{
    // Repository for managing appointments in the health tech application
    public class AppointmentRepository : IRepositoryAppointment
    {
        private readonly HealthTechContext _dbContext;
        private IRepositoryBusinessInterval _businessInterval;

        // Constructor initializes the database context
        public AppointmentRepository()
        {
            _dbContext = new HealthTechContext();
            _businessInterval = new BusinessIntervalRepository() ?? throw new Exception();
        }

        // Adds a new appointment to the repository
        public Task<Appointment> addAppointment(Appointment @appointment)
        {
            // Get the maximum appointment id in the database
            var maxId = _dbContext.Set<Appointment>().Max(u => (int?)u.AppointmentId);
            if (maxId.HasValue)
            {
                appointment.AppointmentId = (int)maxId + 1;
            }
            else
            {
                appointment.AppointmentId = 0;
            }
            // Add the new appointment to the database
            var patient = _dbContext.Set<ApplicationUser>().Find(appointment.PatientId);
            var doctor = _dbContext.Set<Doctor>().Find(appointment.DoctorId);
            appointment.Doctor = doctor;
            appointment.Patient = patient;
            var points = patient.PointsUsed != null ? findAllPastAppointmentsByUserId(patient.UserId).Result * 10 - patient.PointsUsed: findAllPastAppointmentsByUserId(patient.UserId).Result * 10;
            if (patient != null)
            {
                patient.Points = points;
                if (patient.PointsUsed == null)
                    patient.PointsUsed = 0;
            }
            var userEntry = _dbContext.Appointments.Add(appointment);
            
            // Save changes to the database
            _dbContext.SaveChanges();

            // ResetIdentitySeedForTable(this.GetType().Name, maxId);

            // Return the response from task
            return Task.FromResult(appointment);
        }

        // Deletes an appointment from the repository
        public void Delete(int id)
        {
            var appointment = _dbContext.Set<Appointment>().Find(id);
            // Get the maximum appointment id in the database
            var maxId = _dbContext.Set<Appointment>().Max(u => (int?)u.AppointmentId);

            TimeSpan oneDay = appointment.AppointmentDate.Value - DateTime.Today;
            // Remove the appointment from the database if there's more than 24 hours until it
            if(oneDay.TotalHours >= 24)
            {
                _dbContext.Remove(appointment);
            }
            var patient = _dbContext.Set<ApplicationUser>().Find(appointment.PatientId);
            if (patient != null)
            {
                //if (patient.Points == null || patient.Points == 200)
                //{
                //  patient.Points = 0;
                //}
                if(patient.Points != 0)
                    patient.Points -= 10;

            }
            else
            {
                throw new Exception("Appointment cannot be deleted as it is scheduled to occur within the next 24 hours. Appointments must be canceled at least 24 hours in advance.");
            }

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Appointment>> findAllAppointmentsByUserId(int id)
        {
            //IEnumerable<Appointment> appointments = _dbContext.Appointments.Where(u => u.PatientId == id);
            return await _dbContext.Appointments.Where(u => u.PatientId == id).ToListAsync();
            //return appointments;
        }
        public async Task<int> findAllPastAppointmentsByUserId(int id)
        {
            //IEnumerable<Appointment> appointments = _dbContext.Appointments.Where(u => u.PatientId == id);
            return await _dbContext.Appointments.Where(u => u.PatientId == id && u.AppointmentDate < DateTime.Now).CountAsync(); 
            //return appointments;
        }

        public async Task<IEnumerable<Appointment>> findAllAppointmentsByDoctorId(int id)
        {
            //IEnumerable<Appointment> appointments =
            return await _dbContext.Appointments.Where(u => u.DoctorId == id).ToListAsync();
            //return appointments;
        }

        public async Task<IEnumerable<Appointment>> findUpcomingAppointmentsByUserId(int id)
        {
            //IEnumerable<Appointment> appointments = _dbContext.Appointments.Where(u => u.PatientId == id);
            //return appointments;
            return await _dbContext.Appointments.Where(u => u.PatientId == id).Where(u => u.AppointmentDate >= DateTime.Now).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> findUpcomingAppointmentsByDoctorId(int id)
        {
            //IEnumerable<Appointment> appointments =
            return await _dbContext.Appointments.Where(u => u.DoctorId == id).Where(u => u.AppointmentDate >= DateTime.Now).ToListAsync();
            //return appointments;
        }

        // Retrieves all appointments from the repository
        public IEnumerable<Appointment> GetAll()
        {
            // Retrieve all appointments from the database
            var @appointments = _dbContext.Appointments.ToList();
            return @appointments;
        }

        // Retrieves an appointment by its id from the repository
        public async Task<Appointment> GetById(int id)
        {
            // Find the appointment in the database by its id
            var @appointment = _dbContext.Appointments.FirstOrDefault(u => u.AppointmentId == id);

            // Return null if the appointment is not found
            if (@appointment == null)
            {
                return null;
            }

            return @appointment;
        }

        // Updates an existing appointment in the repository
        public Appointment Update(Appointment @appointment)
        {
            // Update the appointment in the database
            var appointmentEntry = _dbContext.Update(@appointment);

            // Save changes to the database
            _dbContext.SaveChanges();

            // Retrieve the updated appointment from the database
            Appointment u = appointmentEntry.Entity;
            return u;
        }


        // The following method finds and returns available time spans for appointments
        // on a specific date for a given doctor ID.
        public async Task<List<TimeSpan>> findAvailableTimeSpanForAppointmentByDateAndDoctorId(int id, DateTime date)
        {
            // Extract the start and end times from the business interval.
            TimeSpan startTime = (TimeSpan)_businessInterval.GetById(id).Result.StartTime;
            TimeSpan endTime = (TimeSpan)_businessInterval.GetById(id).Result.EndTime;
                // Generate a list of time spans within the specified interval with a duration of 1 hour.
            List<TimeSpan> timeSpans = GenerateTimeSpans(startTime, endTime, TimeSpan.FromHours(1));
                // Initialize a list to store available time spans.
            List<TimeSpan> timeSpans2 = new List<TimeSpan>();
                // Iterate through each time span and check if there's an existing appointment
                // for the given doctor ID and date + time span.
            foreach (TimeSpan span in timeSpans) {
                var @appointment = _dbContext.Appointments.FirstOrDefault(a => a.DoctorId == id && a.AppointmentDate == date + span);
                // If no appointment exists for the current time span, add it to the list of available time spans.
                if( @appointment == null)
                {
                    timeSpans2.Add(span);
                }
            }
            // Return the list of available time spans.
            return timeSpans2;

        }


        // This static method generates a list of time spans within a specified range
        // with a given interval between each time span.
        static List<TimeSpan> GenerateTimeSpans(TimeSpan start, TimeSpan end, TimeSpan interval)
        {
            // Initialize a list to store the generated time spans.
            List<TimeSpan> timeSpans = new List<TimeSpan>();

            // Start with the provided start time.
            TimeSpan current = start;

            // Continue adding time spans to the list until reaching or exceeding the end time.
            while (current <= end)
            {
                // Add the current time span to the list.
                timeSpans.Add(current);
                // Move to the next time span by adding the specified interval.
                current = current.Add(interval);
            }

            
            return timeSpans;
        }
    }
}
