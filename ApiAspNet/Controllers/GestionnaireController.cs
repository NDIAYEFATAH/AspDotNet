using ApiAspNet.Models.Gestionnaire;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionnaireController : ControllerBase
    {
        private readonly IGestionnaireService _gestionnaireService;
        private readonly IMapper _mapper;

        public GestionnaireController(
            IGestionnaireService gestionnaireService,
            IMapper mapper)
        {
            _gestionnaireService = gestionnaireService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var gestionnaires = _gestionnaireService.GetAll();
            return Ok(gestionnaires);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var gestionnaire = _gestionnaireService.GetById(id);
            return Ok(gestionnaire);
        }

        [HttpPost]
        public IActionResult Create(CreateRequestGes model)
        {
            _gestionnaireService.Create(model);
            return Ok(new { message = "Gestionnaire créé" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequests model)
        {
            _gestionnaireService.Update(id, model);
            return Ok(new { message = "Gestionnaire mis à jour" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _gestionnaireService.Delete(id);
            return Ok(new { message = "Gestionnaire supprimé" });
        }
    }
}
