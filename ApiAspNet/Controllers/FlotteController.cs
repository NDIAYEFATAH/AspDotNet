using ApiAspNet.Models.Flottes;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlotteController : ControllerBase
    {
        private IFlotteService _FlotteService;
        private IMapper _mapper;

        public FlotteController(
            IFlotteService flotteService,
            IMapper mapper)
        {
            _FlotteService = flotteService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var flottes = _FlotteService.GetAll();
            return Ok(flottes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var flotte = _FlotteService.GetById(id);
            return Ok(flotte);
        }

        [HttpPost]
        public IActionResult Create(CreateRequest model)
        {
            _FlotteService.Create(model);
            return Ok(new { message = "Flotte created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _FlotteService.Update(id, model);
            return Ok(new { message = "Flotte updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _FlotteService.Delete(id);
            return Ok(new { message = "Flotte deleted" });
        }
    }
}
