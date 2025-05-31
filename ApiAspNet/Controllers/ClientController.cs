using ApiAspNet.Models.client;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _clientService.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _clientService.GetById(id);
            return Ok(client);
        }

        [HttpPost]
        public IActionResult Create(CreateClientRequest model)
        {
            _clientService.Create(model);
            return Ok(new { message = "Client créé" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateClientRequest model)
        {
            _clientService.Update(id, model);
            return Ok(new { message = "Client mis à jour" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clientService.Delete(id);
            return Ok(new { message = "Client supprimé" });
        }
    }
}
