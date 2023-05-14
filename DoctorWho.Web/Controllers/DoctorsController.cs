using System.Text.Json;
using AutoMapper;
using DoctorWho.Db;
using DoctorWho.Db.Repositories.Implementations;
using DoctorWhoDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        const int maximumPageSize = 5;
        public DoctorsController(IDoctorRepository repository, IMapper mapper) {
           _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

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
    }
}
