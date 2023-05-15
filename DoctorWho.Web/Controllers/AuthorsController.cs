using AutoMapper;
using DoctorWho.Db;
using DoctorWhoDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AuthorValidator _validator;
        private readonly IAuthorRepository _repository;

        public AuthorsController(IMapper mapper, IAuthorRepository authorRepository)
        {
            _mapper = mapper;  
            _repository = authorRepository;
            _validator = new AuthorValidator();
        }


        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> UpdateAuthor([FromBody] AuthorDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            var validationResult = _validator.Validate(author);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            bool exists = await _repository.AuthorExistsAsync(author.AuthorId);
            if (!exists)
            {
                return NotFound();
            }

            await _repository.UpdateAuthorAsync(author);

            var returnedAuthor = _mapper.Map<AuthorDTO>(author);
            return Ok(returnedAuthor);
        }
    }
}
