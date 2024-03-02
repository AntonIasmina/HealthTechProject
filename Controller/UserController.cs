using HealthTech331.Models;
using HealthTech331.Models.DTO;
using HealthTech331.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace HealthTech331.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Repository for accessing and managing user-related data.
        private readonly IRepositoryUser _userRepository;
        // Repository for accessing and managing appointment-related data.
        private readonly IRepositoryAppointment _appointmentRepository;
        // Repository for accessing and managing doctor-related data.
        private readonly IRepostoryDoctor _doctorRepository;

        public UserController(IRepositoryUser userRepository, IRepositoryAppointment appointmentRepository, IRepostoryDoctor doctorRepository)
        {
            // Dependency injection to inject user and appointment repositories
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(_appointmentRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(_doctorRepository));
        }

        [HttpGet]
        public ActionResult<List<ApplicationUser>> GetAll()
        {
            // Retrieve all users and return the result
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                // Attempt to get the user by id
                var _user = await _userRepository.GetById(id);
                if (_user == null)
                {
                    // If the user is not found, return a 404 Not Found status
                    return NotFound();
                }

                // Delete the user and return a 204 No Content status
                _userRepository.Delete(_user);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Return a 400 Bad Request status with the exception message in case of an error
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetById(int id)
        {
            // Get a user by id and return the result
           
            var u =  await _userRepository.GetById(id );
            u.Points = u.PointsUsed != null ? (_appointmentRepository.findAllPastAppointmentsByUserId(id).Result * 10 - u.PointsUsed) : _appointmentRepository.findAllPastAppointmentsByUserId(id).Result * 10;
            return Ok(u);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserDTO user)
        {
            try
            {
                // Create a new ApplicationUser object from the UserDTO received in the request
                var userToAdd = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    Cnp = user.Cnp,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };

                // Add the new user to the repository asynchronously
                await _userRepository.addUser(userToAdd);

                // Return a 200 OK status
                return Ok();
            }
            catch (Exception ex)
            {
                // Return a 400 Bad Request status with the exception message in case of an error
                return BadRequest(ex.Message);
            }
        }

[HttpPost]
        public async Task<ActionResult<AppointmentDTO>> SaveAppointment([FromBody] AppointmentDTO model)
        {
            try
            {
                // Create a new Appointment object from the AppointmentDTO received in the request
                var appointmentToAdd = new Appointment
                {
                    AppointmentDate = model.AppointmentDate,
                    AppointmentDescription = model.AppointmentDescription,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    Discount = model.Discount
                };


                if (model.UsedPoints != 0)
                    _userRepository.updatePoints(model.PatientId, model.UsedPoints);
                // Add the new appointment to the repository asynchronously
                await _appointmentRepository.addAppointment(appointmentToAdd);
                // Return a 200 OK status
                return Ok();
            }
            catch (Exception ex)
            {
                // Return a 400 Bad Request status with the exception message in case of an error
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAppointment( int id)
        {
            try
            {
                // Attempt to delete the appointment from the repository using the provided ID
                _appointmentRepository.Delete(id);
                // Return a 200 OK response indicating a successful deletion
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAppointmentsByUserId(int id)
        {
            try
            {
                 // Retrieve all appointments for a given user ID from the repository
                var model = (await _appointmentRepository.findAllAppointmentsByUserId(id)).Select(async s =>
                {
                    // For each appointment, retrieve doctor information if DoctorId is not null
                    var doctor = s.DoctorId != null ? await _doctorRepository.GetById(s.DoctorId ?? 0) : null;
                    // Create an anonymous object with relevant appointment and doctor information
                    return new
                    {
                        AppointmentDate = s.AppointmentDate ?? new DateTime(),
                        AppointmentDescription = s.AppointmentDescription ?? "-",
                        DoctorFirstName = doctor?.FirstName ?? "-",
                        DoctorLastName = doctor?.LastName ?? "-",
                        DoctorId = s.DoctorId ?? 0,
                        AppointmentId = s.AppointmentId,
                        Discount = s.Discount ?? 0

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
        public async Task<IActionResult> GetUpcomingAppointmentsByUserId(int id)
        {
            try
            {
                // Retrieve upcoming appointments for a given user ID from the repository
                var model = (await _appointmentRepository.findUpcomingAppointmentsByUserId(id)).Select(async s =>
                {
                    // For each upcoming appointment, retrieve doctor information if DoctorId is not null
                    var doctor = s.DoctorId != null ? await _doctorRepository.GetById(s.DoctorId ?? 0) : null;
                    // Create an anonymous object with relevant upcoming appointment and doctor information
                    return new
                    {
                        AppointmentDate = s.AppointmentDate ?? new DateTime(),
                        AppointmentDescription = s.AppointmentDescription ?? "-",
                        DoctorFirstName = doctor?.FirstName ?? "-",
                        DoctorLastName = doctor?.LastName ?? "-",
                        DoctorId = s.DoctorId ?? 0
                    };
                }).ToList();
                // Return a 200 OK response with the constructed model containing upcoming appointment details
                return Ok(model);
            }
            catch (Exception ex)
            {
                // In case of an exception, return a null response (consider logging the exception for debugging)
                return null;
            }

        }


        // HTTP GET endpoint to find and return available time spans for appointments
        // based on a specific doctor ID and date.
       [HttpGet("FindAvailableTimeSpanForAppointmentByDateAndDoctorId")]
        public async Task<List<TimeSpan>> FindAvailableTimeSpanForAppointmentByDateAndDoctorId(int id,DateTime date)
        {
                // Call the repository method to find available time spans for appointments based on doctor ID and date
            return await _appointmentRepository.findAvailableTimeSpanForAppointmentByDateAndDoctorId(id, date);

        }
    }
}
