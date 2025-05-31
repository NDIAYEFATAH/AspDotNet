using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Gestionnaire;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IGestionnaireService
    {
        IEnumerable<Gestionnaire> GetAll();
        Gestionnaire GetById(int id);
        void Create(CreateRequestGes model);
        void Update(int id, UpdateRequests model);
        void Delete(int id);
    }

    public class GestionnaireService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public GestionnaireService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Gestionnaire> GetAll()
        {
            return _context.Gestionnaires;
        }

        public Gestionnaire GetById(int id)
        {
            return getGestionnaire(id);
        }

        public void Create(CreateRequestGes model)
        {


            // map model to new user object 
            var gestionnaire = _mapper.Map<Gestionnaire>(model);

            // save flotte 
            _context.Gestionnaires.Add(gestionnaire);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequests model)
        {
            var gestionnaire = getGestionnaire(id);

            _mapper.Map(model, gestionnaire);
            _context.Gestionnaires.Update(gestionnaire);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var gestionnaire = getGestionnaire(id);
            _context.Gestionnaires.Remove(gestionnaire);
            _context.SaveChanges();
        }

        // helper methods 

        private Gestionnaire getGestionnaire(int id)
        {
            var gestionnaire = _context.Gestionnaires.Find(id);
            if (gestionnaire == null) throw new KeyNotFoundException("Flotte not found");
            return gestionnaire;
        }
    }
}
