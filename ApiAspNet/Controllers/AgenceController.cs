using ApiAspNet.Models.Agence;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenceController : ControllerBase
    {
        private readonly IAgenceService _agenceService;
        private readonly IMapper _mapper;

        public AgenceController(IAgenceService agenceService, IMapper mapper)
        {
            _agenceService = agenceService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var agences = _agenceService.GetAll();
            return Ok(agences);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var agence = _agenceService.GetById(id);
            return Ok(agence);
        }

        [HttpPost]
        public IActionResult Create(CreateAgenceRequest model)
        {
            _agenceService.Create(model);
            return Ok(new { message = "Agence créée" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateAgenceRequest model)
        {
            _agenceService.Update(id, model);
            return Ok(new { message = "Agence mise à jour" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _agenceService.Delete(id);
            return Ok(new { message = "Agence supprimée" });
        }
    }
}
