using ApiAspNet.Models.offre;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffreControlller : ControllerBase
    {
        private readonly IOffreService _offreService;
        private readonly IMapper _mapper;

        public OffreControlller(IOffreService offreService, IMapper mapper)
        {
            _offreService = offreService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var offres = _offreService.GetAll();
            return Ok(offres);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var offre = _offreService.GetById(id);
            return Ok(offre);
        }

        [HttpPost]
        public IActionResult Create(CreateOffreRequest model)
        {
            _offreService.Create(model);
            return Ok(new { message = "Offre créée" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateOffreRequest model)
        {
            _offreService.Update(id, model);
            return Ok(new { message = "Offre mise à jour" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _offreService.Delete(id);
            return Ok(new { message = "Offre supprimée" });
        }
    }
}
