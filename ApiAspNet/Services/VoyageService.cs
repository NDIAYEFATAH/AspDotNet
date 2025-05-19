using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Voyages;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IVoyageService
    {
        IEnumerable<Voyage> GetAll();
        Voyage GetById(int id);
        void Create(CreateRequestV model);
        void Update(int id, UpdateRequestV model);
        void Delete(int id);
    }
    public class VoyageService : IVoyageService
    {

        private DataContext _context;
        private readonly IMapper _mapper;

        public VoyageService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Voyage> GetAll()
        {
            return _context.Voyages;
        }

        public Voyage GetById(int id)
        {
            return getVoyage(id);
        }

        public void Create(CreateRequestV model)
        {


            // map model to new user object 
            var voyage = _mapper.Map<Voyage>(model);

            // save flotte 
            _context.Voyages.Add(voyage);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequestV model)
        {
            var voyage = getVoyage(id);

            _mapper.Map(model, voyage);
            _context.Voyages.Update(voyage);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var voyage = getVoyage(id);
            _context.Voyages.Remove(voyage);
            _context.SaveChanges();
        }

        // helper methods 

        private Voyage getVoyage(int id)
        {
            var voyage = _context.Voyages.Find(id);
            if (voyage == null) throw new KeyNotFoundException("Voyage not found");
            return voyage;
        }
    }
}
