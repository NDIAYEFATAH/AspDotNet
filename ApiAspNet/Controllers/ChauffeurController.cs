using ApiAspNet.Models.chauffeur;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChauffeurController : ControllerBase
    {
        private readonly IChauffeurService _chauffeurService;
        private readonly IMapper _mapper;

        public ChauffeurController(IChauffeurService chauffeurService, IMapper mapper)
        {
            _chauffeurService = chauffeurService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var chauffeurs = _chauffeurService.GetAll();
            return Ok(chauffeurs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var chauffeur = _chauffeurService.GetById(id);
            return Ok(chauffeur);
        }

        [HttpPost]
        public IActionResult Create(CreateChauffeurRequest model)
        {
            _chauffeurService.Create(model);
            return Ok(new { message = "Chauffeur créé" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateChauffeurRequest model)
        {
            _chauffeurService.Update(id, model);
            return Ok(new { message = "Chauffeur mis à jour" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _chauffeurService.Delete(id);
            return Ok(new { message = "Chauffeur supprimé" });
        }
    }
}
