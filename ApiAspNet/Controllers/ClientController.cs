using ApiAspNet.Models.client;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IKafkaProducerService _kafkaProducer;

        public ClientController(IClientService clientService, IMapper mapper, IKafkaProducerService kafkaProducer)
        {
            _clientService = clientService;
            _mapper = mapper;
            _kafkaProducer = kafkaProducer;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Log.Information("🔥 Ceci est un test manuel de log pour ELK !");
            //return Ok("Log envoyé !");
            var clients = _clientService.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>  GetById(int id)
        {
            var client = _clientService.GetById(id);
            await _kafkaProducer.PublishAsync("clients", new
        {
            EventType = "Lecture d'un Client",
            ClientId = id,
            Timestamp = DateTime.UtcNow
        });
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientRequest model)
        {
            _clientService.Create(model);
            await _kafkaProducer.PublishAsync("clients", new
            {
                EventType = "Client créé",
                Data = model,
                Timestamp = DateTime.UtcNow
            });
            return Ok(new { message = "Client créé" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateClientRequest model)
        {
            _clientService.Update(id, model);
            await _kafkaProducer.PublishAsync("clients", new
            {
                EventType = "Client mis à jour",
                ClientId = id,
                Timestamp = DateTime.UtcNow
            });
            return Ok(new { message = "Client mis à jour" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _clientService.Delete(id);
            await _kafkaProducer.PublishAsync("clients", new
            {
                EventType = "Client supprimé",
                ClientId = id,
                Timestamp = DateTime.UtcNow
            });
            return Ok(new { message = "Client supprimé" });
        }
    }
}
