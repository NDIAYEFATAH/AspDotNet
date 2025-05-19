using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Flottes;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IFlotteService
    {
        IEnumerable<Flotte> GetAll();
        Flotte GetById(int id);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }
    public class FlotteService : IFlotteService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public FlotteService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Flotte> GetAll()
        {
            return _context.Flottes;
        }

        public Flotte GetById(int id)
        {
            return getFlotte(id);
        }

        public void Create(CreateRequest model)
        {


            // map model to new user object 
            var flotte = _mapper.Map<Flotte>(model);

            // save flotte 
            _context.Flottes.Add(flotte);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var flotte = getFlotte(id);

            _mapper.Map(model, flotte);
            _context.Flottes.Update(flotte);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var flotte = getFlotte(id);
            _context.Flottes.Remove(flotte);
            _context.SaveChanges();
        }

        // helper methods 

        private Flotte getFlotte(int id)
        {
            var flotte = _context.Flottes.Find(id);
            if (flotte == null) throw new KeyNotFoundException("Flotte not found");
            return flotte;
        }
    }
}
