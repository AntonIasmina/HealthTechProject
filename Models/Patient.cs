using System;
using System.Collections.Generic;

namespace HealthTech331.Models
{
    // Patient class represents a patient in the health tech application
    public partial class Patient
    {
        // Constructor initializes the Appointments collection
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        // Unique identifier for the patient, referencing the associated ApplicationUser
        public int UserId { get; set; }

        // Navigation property representing the associated ApplicationUser for the patient (not null)
        public virtual ApplicationUser User { get; set; } = null!;

        // Navigation property representing the collection of appointments associated with the patient
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
