using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Reservations;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        void Create(CreateReservationRequest model);
        void Update(int id, UpdateReservationRequest model);
        void Delete(int id);
    }
    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReservationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetById(int id)
        {
            return getReservation(id);
        }

        public void Create(CreateReservationRequest model)
        {
            var reservation = _mapper.Map<Reservation>(model);

            // Récupère le client (si besoin pour valider qu'il existe)
            var client = _context.Clients.Find(model.ClientId);
            if (client == null)
                throw new KeyNotFoundException("Client non trouvé");

            reservation.Client = client;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateReservationRequest model)
        {
            var reservation = getReservation(id);

            if (model.ClientId.HasValue)
            {
                var client = _context.Clients.Find(model.ClientId.Value);
                if (client == null)
                    throw new KeyNotFoundException("Client non trouvé");

                reservation.Client = client;
            }

            _mapper.Map(model, reservation);

            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var reservation = getReservation(id);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        private Reservation getReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
                throw new KeyNotFoundException("Réservation non trouvée");
            return reservation;
        }
    }
}
