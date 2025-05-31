using ApiAspNet.Models.Reservations;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reservations = _reservationService.GetAll();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservation = _reservationService.GetById(id);
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult Create(CreateReservationRequest model)
        {
            _reservationService.Create(model);
            return Ok(new { message = "Réservation créée" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateReservationRequest model)
        {
            _reservationService.Update(id, model);
            return Ok(new { message = "Réservation mise à jour" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _reservationService.Delete(id);
            return Ok(new { message = "Réservation supprimée" });
        }
    }
}
