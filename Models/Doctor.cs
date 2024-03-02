using System;
using System.Collections.Generic;

namespace HealthTech331.Models
{
    // Doctor class represents a healthcare professional in the health tech application
    public partial class Doctor
    {
        // Constructor initializes the Appointments collection
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
        }

        // Unique identifier for the doctor
        public int DoctorId { get; set; }

        // Foreign key referencing the speciality associated with the doctor (can be null)
        public int? SpecialityId { get; set; }

        // Doctor's first name (can be null)
        public string? FirstName { get; set; }

        // Doctor's last name (can be null)
        public string? LastName { get; set; }

        // Doctor's personal identification number (can be null)
        public int? Cnp { get; set; }

        // Doctor's email address (can be null)
        public string? Email { get; set; }

        // Doctor's username (can be null)
        public string? UserName { get; set; }

        // Doctor's password (can be null)
        public string? Password { get; set; }

        // Foreign key referencing the business interval associated with the doctor (can be null)
        public int? BusinessIntervalId { get; set; }

        // Navigation property representing the associated business interval (can be null)
        public virtual BusinessInterval? BusinessInterval { get; set; }

        // Navigation property representing the associated speciality (can be null)
        public virtual Speciality? Speciality { get; set; }

        // Navigation property representing the collection of appointments associated with the doctor
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
