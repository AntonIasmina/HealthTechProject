using HealthTech331.Models.DTO;
using HealthTech331.Models;
using HealthTech331.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HealthTech331.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        // Repository for accessing and managing doctor-related data.
        private readonly IRepostoryDoctor _doctorRepository;
        // Repository for accessing and managing appointment-related data.
        private readonly IRepositoryAppointment _appointmentRepository;
        // Repository for accessing and managing user-related data.
        private readonly IRepositoryUser _userRepository;

        public DoctorController(IRepostoryDoctor doctorRepository, IRepositoryAppointment appointmentRepository, IRepositoryUser userRepository)
        {
            // Dependency injection to inject the repository for doctors
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(_doctorRepository));
            // Dependency injection to inject the repository for appointments.
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(_appointmentRepository));
            // Dependency injection to inject the repository for users.
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
        }

        [HttpGet]
        public ActionResult<List<ApplicationUser>> GetAll()
        {
            // Retrieve all doctors and return the result
            var users = _doctorRepository.GetAll();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
              
                var _user = await _doctorRepository.GetById(id);
                if (_user == null)
                {
                    // If the doctor is not found, return a 404 Not Found status
                    return NotFound();
                }
                
                // Delete the doctor and return a 204 No Content status
                _doctorRepository.Delete(_user);
                return NoContent();

            }
            catch (Exception ex)
            {
                // Return a 400 Bad Request status with the exception message in case of an error
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> GetById(int id)
        {
            // Get a doctor by id and return the result
            var u = _doctorRepository.GetById(id);
            return Ok(u);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> AddDoctor([FromBody] DoctorDTO doctor)
        {
            try
            {
                // Create a new Doctor object from the DoctorDTO received in the request
                var doctorToAdd = new Doctor
                {
                    Email = doctor.Email,
                    Cnp = doctor.Cnp,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    UserName = doctor.UserName,
                    Password = doctor.Password,
                };

                // Add the new doctor to the repository asynchronously
                await _doctorRepository.addDoctor(doctorToAdd);

                // Return a 200 OK status
                return Ok();

            }
            catch (Exception ex)
            {
                // Return a 400 Bad Request status with the exception message in case of an error
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAppointmentsByDoctorId(int id)
        {
            try
            {
                // Retrieve all appointments for a given doctor ID from the repository
                var model = (await _appointmentRepository.findAllAppointmentsByDoctorId(id)).Select(async s =>
                {
                    // For each appointment, retrieve patient information if PatientId is not null
                    var user = s.PatientId != null ? await _userRepository.GetById(s.PatientId ?? 0) : null;
                    // Create an anonymous object with relevant appointment and user information
                    return new
                    {
                        AppointmentDate = s.AppointmentDate ?? new DateTime(),
                        AppointmentDescription = s.AppointmentDescription ?? "-",
                        UserFirstName = user?.FirstName ?? "-",
                        UserLastName = user?.LastName ?? "-",
                    };
                }).ToList();
                // Return a 200 OK response with the constructed model containing appointment details
                return Ok(model);
            }
            catch (Exception ex)
            {
                // In case of an exception, return a null response (consider logging the exception for debugging)
                return null;
            }

        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUpcomingAppointmentsByDoctorId(int id)
        {
            try
            {
                // Retrieve upcoming appointments for a given doctor ID from the repository
                var model = (await _appointmentRepository.findUpcomingAppointmentsByDoctorId(id)).Select(async s =>
                {
                    // For each upcoming appointment, retrieve patient information if PatientId is not null
                    var user = s.PatientId != null ? await _userRepository.GetById(s.PatientId ?? 0) : null;
                    // Create an anonymous object with relevant upcoming appointment and user information
                    return new
                    {
                        AppointmentDate = s.AppointmentDate ?? new DateTime(),
                        AppointmentDescription = s.AppointmentDescription ?? "-",
                        UserFirstName = user?.FirstName ?? "-",
                        UserLastName = user?.LastName ?? "-"
                    };
                }).ToList();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        // Call the GetAllDoctorsBySpecialityId method from the _doctorRepository
        // to retrieve a list of doctors associated with the given speciality ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllDoctorBySpecialityId(int id)
        {
            // Call the GetAllDoctorsBySpecialityId method from the _doctorRepository
            // to retrieve a list of doctors associated with the given speciality ID.
            var users = _doctorRepository.GetAllDoctorsBySpecialityId(id);
                // Return an HTTP 200 OK response along with the list of doctors.
            return Ok(users);

        }

    }
}
