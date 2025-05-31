using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.offre;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IOffreService
    {
        IEnumerable<Offre> GetAll();
        Offre GetById(int id);
        void Create(CreateOffreRequest model);
        void Update(int id, UpdateOffreRequest model);
        void Delete(int id);
    }
    public class OffreService 
    {
        /*private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OffreService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Offre> GetAll()
        {
            return _context.Offres.ToList();
        }

        public Offre GetById(int id)
        {
            return getOffre(id);
        }

        public void Create(CreateOffreRequest model)
        {
            var offre = _mapper.Map<Offre>(model);

            _context.Offres.Add(offre);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateOffreRequest model)
        {
            var offre = getOffre(id);

            _mapper.Map(model, offre);

            _context.Offres.Update(offre);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var offre = getOffre(id);
            _context.Offres.Remove(offre);
            _context.SaveChanges();
        }

        private Offre getOffre(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre == null)
                throw new KeyNotFoundException("Offre non trouvée");
            return offre;
        }*/
    }
}
