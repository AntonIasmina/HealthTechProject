using System;
using System.Collections.Generic;

namespace HealthTech331.Models
{
    // Speciality class represents a medical specialization in the health tech application
    public partial class Speciality
    {
        // Constructor initializes the Doctors collection
        public Speciality()
        {
            Doctors = new HashSet<Doctor>();
        }

        // Unique identifier for the speciality
        public int SpecialityId { get; set; }

        // Name of the speciality (can be null)
        public string? SpecialityName { get; set; }

        // Description of the speciality (can be null)
        public string? SpecialityDescription { get; set; }

        // Navigation property representing the collection of doctors associated with this speciality
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
