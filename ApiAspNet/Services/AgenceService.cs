using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Agence;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IAgenceService
    {
        IEnumerable<Agence> GetAll();
        Agence GetById(int id);
        void Create(CreateAgenceRequest model);
        void Update(int id, UpdateAgenceRequest model);
        void Delete(int id);
    }
    public class AgenceService : IAgenceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AgenceService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Agence> GetAll()
        {
            return _context.Agences.ToList();

        }


        public Agence GetById(int id)
        {
            return getAgence(id);
        }

        public void Create(CreateAgenceRequest model)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(model);
                Console.WriteLine("CreateAgenceRequest reçu: " + json);

                var agence = _mapper.Map<Agence>(model);
                _context.Agences.Add(agence);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException != null ? ex.InnerException.Message : "";
                throw new Exception("Erreur lors de la création de l'agence : " + ex.Message + " Détails : " + inner, ex);
            }
        }




        public void Update(int id, UpdateAgenceRequest model)
        {
            var agence = getAgence(id);

            _mapper.Map(model, agence);

            _context.Agences.Update(agence);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var agence = getAgence(id);
            _context.Agences.Remove(agence);
            _context.SaveChanges();
        }

        // Méthode privée pour récupérer l'agence
        private Agence getAgence(int id)
        {
            var agence = _context.Agences.Find(id);
            if (agence == null)
                throw new KeyNotFoundException("Agence not found");
            return agence;
        }
    }
}
