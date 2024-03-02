using System;
using System.Collections.Generic;

namespace HealthTech331.Models
{
    public partial class Appointment
    {
        // Unique identifier for the appointment
        public int AppointmentId { get; set; }

        // Foreign key referencing the patient associated with the appointment (can be null)
        public int? PatientId { get; set; }

        // Foreign key referencing the doctor associated with the appointment (can be null)
        public int? DoctorId { get; set; }

        // Date and time of the appointment (can be null)
        public DateTime? AppointmentDate { get; set; }

        // Description of the appointment (can be null)
        public string? AppointmentDescription { get; set; }
        public int? Discount { get; set; }

        // Navigation property representing the associated doctor (can be null)
        public virtual Doctor? Doctor { get; set; }

        // Navigation property representing the associated patient (can be null)
        public virtual ApplicationUser? Patient { get; set; }
    }
}
