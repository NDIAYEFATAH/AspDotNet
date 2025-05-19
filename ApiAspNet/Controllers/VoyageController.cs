using ApiAspNet.Models.Voyages;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoyageController : ControllerBase
    {
        private IVoyageService _VoyageService;
        private IMapper _mapper;

        public VoyageController(
            IVoyageService voyageService,
            IMapper mapper)
        {
            _VoyageService = voyageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var voyages = _VoyageService.GetAll();
            return Ok(voyages);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var voyage = _VoyageService.GetById(id);
            return Ok(voyage);
        }

        [HttpPost]
        public IActionResult Create(CreateRequestV model)
        {
            _VoyageService.Create(model);
            return Ok(new { message = "Voyage created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequestV model)
        {
            _VoyageService.Update(id, model);
            return Ok(new { message = "Voyage updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _VoyageService.Delete(id);
            return Ok(new { message = "Voyage deleted" });
        }
    }
}
