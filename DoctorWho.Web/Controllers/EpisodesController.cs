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
        private readonly IEnemyRepository _enemyRepository;
        private readonly ICompanionRepository _companionRepository; 
        private readonly IMapper _mapper;
        private readonly EpisodeValidator _episodeValidator;
        private readonly EnemyValidator _enemyValidator;
        private readonly CompanionValidator _companionValidator;
        const int maximumPageSize = 5;
        public EpisodesController(IEpisodeRepository repository, IMapper mapper, 
            IAuthorRepository authorRepository, IDoctorRepository doctorRepository, IEnemyRepository enemyRepository, ICompanionRepository companionRepository)
        {
            _episodeRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _episodeValidator = new EpisodeValidator() ?? throw new ArgumentNullException(nameof(mapper));
            _enemyValidator = new EnemyValidator() ?? throw new ArgumentNullException(nameof(mapper));
            _companionValidator = new CompanionValidator() ?? throw new ArgumentNullException(nameof(mapper));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _enemyRepository = enemyRepository ?? throw new ArgumentNullException(nameof(enemyRepository));
            _companionRepository = companionRepository ?? throw new ArgumentNullException(nameof(companionRepository));
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
                return NotFound();
            }

            if (!await _doctorRepository.DoctorExistsAsync((int)episode.DoctorId))
            {
                return NotFound();
            }

            await _episodeRepository.CreateEpisodeAsync(episode);
            return Ok(_mapper.Map<EpisodeDTO>(episode));
        }

        [HttpPost("{episodeId}/enemies")]
        public async Task<ActionResult> AddEnemyToEpisode(int episodeId,[FromBody] EnemyDTO enemyDTO)
        {
            var enemy = _mapper.Map<Enemy>(enemyDTO);
            var validationResult = _enemyValidator.Validate(enemy);
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            if (!await _episodeRepository.EpisodeExistsAsync(episodeId))
            {
                return NotFound();
            }
            if(!await _enemyRepository.EnemyExistsAsync(enemy.EnemyId))
            {
                return NotFound();
            }

            await _episodeRepository.AddEnemyToEpisodeAsync(enemy, episodeId);

            return Ok();
        }

        [HttpPost("{episodeId}/companions")]
        public async Task<ActionResult> AddCompanionToEpisode(int episodeId, [FromBody] CompanionDTO companionDTO)
        {
            var companion = _mapper.Map<Companion>(companionDTO);
            var validationResult = _companionValidator.Validate(companion);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            if (!await _episodeRepository.EpisodeExistsAsync(episodeId))
            {
                return NotFound();
            }
            if (!await _companionRepository.CompanionExistsAsync(companion.CompanionId))
            {
                return NotFound();
            }
            
            await _episodeRepository.AddCompanionToEpisodeAsync(companion, episodeId);

            return Ok();
        }
    }
}
