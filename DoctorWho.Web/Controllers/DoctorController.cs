using DoctorWho.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _repository;

        public DoctorController(IDoctorRepository repository) {
           _repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetDoctorsAsync()
        {
           var list =  await _repository.GetAvailableDoctorsAsync();
            return new JsonResult(list);
        }
    }
}
