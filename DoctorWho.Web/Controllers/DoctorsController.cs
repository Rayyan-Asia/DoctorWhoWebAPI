using System.Text.Json;
using AutoMapper;
using DoctorWho.Db;
using DoctorWho.Db.Repositories.Implementations;
using DoctorWhoDomain;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        private readonly DoctorValidator _doctorValidator;
        const int maximumPageSize = 5;
        public DoctorsController(IDoctorRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _doctorValidator = new DoctorValidator();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetDoctors(int pageNumber = 0, int pageSize = 2)
        {
            if (pageSize >= 10)
            {
                pageSize = maximumPageSize;
            }

            var (doctors, paginationMetadata) = await _repository.GetAvailableDoctorsAsync(pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<DoctorDTO>>(doctors));
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> UpsertDoctor([FromBody] DoctorDTO doctorDTO)
        {
            var doctor = _mapper.Map<Doctor>(doctorDTO);
            var validationResult = _doctorValidator.Validate(doctor);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (doctorDTO.DoctorId == 0)
            {
                await _repository.CreateDoctorAsync(doctor);
            }
            bool exists = await _repository.DoctorExistsAsync(doctor.DoctorId);
            if (!exists)
            {
                return NotFound();
            }

            await _repository.UpdateDoctorAsync(doctor);

            var returnedDoctor = _mapper.Map<DoctorDTO>(doctor);
            return Ok(returnedDoctor);
        }

        [HttpDelete("{doctorId}")]

        public async Task<ActionResult> DeleteDoctor(int doctorId)
        {
            var doctorExists = await _repository.DoctorExistsAsync(doctorId);
            if (!doctorExists)
            {
                return NotFound();
            }
            await _repository.DeleteDoctorAsync(doctorId);
            return NoContent();
        }

    }
}
