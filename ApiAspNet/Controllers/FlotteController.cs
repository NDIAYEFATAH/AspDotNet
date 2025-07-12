using ApiAspNet.Entities;
using ApiAspNet.Models.Flottes;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlotteController : ControllerBase
    {
        private readonly IFlotteService _FlotteService;
        private readonly IMapper _mapper;
        private readonly ILogger<FlotteController> _logger;
        private readonly IDistributedCache _cache;

        public FlotteController(
            IFlotteService flotteService,
            IMapper mapper,
            ILogger<FlotteController> logger,
            IDistributedCache cache)
        {
            _FlotteService = flotteService;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        // GET: api/Flotte
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Récupération de toutes les flottes (avec cache)");

                const string cacheKey = "flottes_all";
                var cachedData = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedData))
                {
                    var flottes = JsonSerializer.Deserialize<List<Flotte>>(cachedData);
                    _logger.LogInformation("Données récupérées depuis Redis.");
                    return Ok(flottes);
                }

                var flottesFromDb = _FlotteService.GetAll();
                var jsonData = JsonSerializer.Serialize(flottesFromDb);

                await _cache.SetStringAsync(cacheKey, jsonData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

                return Ok(flottesFromDb);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des flottes.");
                return StatusCode(500, new { message = "Une erreur est survenue." });
            }
        }

        // GET: api/Flotte/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var flotte = _FlotteService.GetById(id);
                if (flotte == null)
                {
                    _logger.LogWarning("Flotte avec l'identifiant {Id} non trouvée.", id);
                    return NotFound(new { message = "Flotte not found." });
                }

                return Ok(flotte);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de la flotte avec l'identifiant {Id}.", id);
                return StatusCode(500, new { message = "Une erreur est survenue." });
            }
        }

        // POST: api/Flotte
        [HttpPost]
        public IActionResult Create(CreateRequest model)
        {
            try
            {
                _FlotteService.Create(model);

                // Invalidation du cache
                _cache.Remove("flottes_all");

                _logger.LogInformation("Nouvelle flotte créée avec succès. Cache invalidé.");
                return Ok(new { message = "Flotte created" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création d'une flotte.");
                return StatusCode(500, new { message = "Une erreur est survenue lors de la création." });
            }
        }

        // PUT: api/Flotte/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            try
            {
                _FlotteService.Update(id, model);

                // Invalidation du cache
                _cache.Remove("flottes_all");

                _logger.LogInformation("Flotte mise à jour. Cache invalidé.");
                return Ok(new { message = "Flotte updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour de la flotte avec l'identifiant {Id}.", id);
                return StatusCode(500, new { message = "Une erreur est survenue lors de la mise à jour." });
            }
        }

        // DELETE: api/Flotte/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _FlotteService.Delete(id);

                // Invalidation du cache
                _cache.Remove("flottes_all");

                _logger.LogInformation("Flotte supprimée. Cache invalidé.");
                return Ok(new { message = "Flotte deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression de la flotte avec l'identifiant {Id}.", id);
                return StatusCode(500, new { message = "Une erreur est survenue lors de la suppression." });
            }
        }
    }
}
