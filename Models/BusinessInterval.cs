using System;
using System.Collections.Generic;

namespace HealthTech331.Models
{
    // BusinessInterval class represents a time interval during which doctors have business hours
    public partial class BusinessInterval
    {
        // Constructor initializes the Doctors collection
        public BusinessInterval()
        {
            Doctors = new HashSet<Doctor>();
        }

        // Unique identifier for the business interval
        public int BusinessIntervalId { get; set; }

        // Start time of the business interval (can be null)
        public TimeSpan? StartTime { get; set; }

        // End time of the business interval (can be null)
        public TimeSpan? EndTime { get; set; }

        // Day of the week associated with the business interval (can be null)
        public int? Day { get; set; }

        // Navigation property representing the collection of doctors associated with this business interval
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
