using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.chauffeur;
using ApiAspNet.Models.Flottes;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IChauffeurService
    {
        IEnumerable<Chauffeur> GetAll();
        Chauffeur GetById(int id);
        void Create(CreateChauffeurRequest model);
        void Update(int id, UpdateChauffeurRequest model);
        void Delete(int id);
    }
    public class ChauffeurService : IChauffeurService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ChauffeurService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Chauffeur> GetAll()
        {
            return _context.Chauffeurs.ToList();
        }

        public Chauffeur GetById(int id)
        {
            return getChauffeur(id);
        }

        public void Create(CreateChauffeurRequest model)
        {
            var chauffeur = _mapper.Map<Chauffeur>(model);
            _context.Chauffeurs.Add(chauffeur);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateChauffeurRequest model)
        {
            var chauffeur = getChauffeur(id);

            _mapper.Map(model, chauffeur);

            _context.Chauffeurs.Update(chauffeur);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var chauffeur = getChauffeur(id);
            _context.Chauffeurs.Remove(chauffeur);
            _context.SaveChanges();
        }

        private Chauffeur getChauffeur(int id)
        {
            var chauffeur = _context.Chauffeurs.Find(id);
            if (chauffeur == null)
                throw new KeyNotFoundException("Chauffeur not found");
            return chauffeur;
        }
    }
}
