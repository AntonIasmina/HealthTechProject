using System;
using System.Collections.Generic;

namespace HealthTech331.Models
{
    public partial class ApplicationUser
    {
        // Constructor initializes the Appointments collection
        public ApplicationUser()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int UserId { get; set; }

        // User's username (can be null)
        public string? UserName { get; set; }

        // User's first name (can be null)
        public string? FirstName { get; set; }

        // User's last name (can be null)
        public string? LastName { get; set; }

        // User's personal identification number (can be null)
        public int? Cnp { get; set; }

        // User's password (can be null)
        public string? Password { get; set; }

        // User's email address (can be null)
        public string? Email { get; set; }
        public int? Points { get; set; }
        public int? PointsUsed { get; set; }

        // Navigation property representing the collection of appointments associated with the user
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
