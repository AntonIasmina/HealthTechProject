namespace HealthTech331.Models.DTO
{
    public class AppointmentDTO
    {
        public DateTime? AppointmentDate { get; set; }
        public string? AppointmentDescription { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int Discount { get; set; }
        public int UsedPoints { get; set; }
    }
}
