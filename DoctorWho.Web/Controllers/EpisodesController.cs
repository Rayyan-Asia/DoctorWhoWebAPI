using System.Text.Json;
using AutoMapper;
using DoctorWho.Db;
using DoctorWhoDomain;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EpisodesController : Controller
    {

        private readonly IEpisodeRepository _episodeRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly EpisodeValidator _episodeValidator;
        const int maximumPageSize = 5;
        public EpisodesController(IEpisodeRepository repository, IMapper mapper, IAuthorRepository authorRepository, IDoctorRepository doctorRepository)
        {
            _episodeRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _episodeValidator = new EpisodeValidator();
            _authorRepository = authorRepository;
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EpisodeDTO>>> GetEpisodes(int pageNumber = 0, int pageSize = 2)
        {
            if (pageSize >= 10)
            {
                pageSize = maximumPageSize;
            }

            var (episodes, paginationMetadata) = await _episodeRepository.GetEpisodesAsync(pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<EpisodeDTO>>(episodes));
        }

        [HttpPost]
        public async Task<ActionResult <EpisodeDTO>> CreateEpisode([FromBody] CreateEpisodeDTO episodeDTO)
        {
            var episode = _mapper.Map<Episode>(episodeDTO);
            var validationResult = _episodeValidator.Validate(episode);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            if(!await _authorRepository.AuthorExistsAsync(episode.AuthorId))
            {
                return BadRequest($"Author with ID {episode.AuthorId} does not Exist");
            }

            if (!await _doctorRepository.DoctorExistsAsync((int)episode.DoctorId))
            {
                return BadRequest($"Doctor with ID {episode.DoctorId} does not Exist");
            }

            await _episodeRepository.CreateEpisodeAsync(episode);
            return Ok(_mapper.Map<EpisodeDTO>(episode));
        }
    }
}
