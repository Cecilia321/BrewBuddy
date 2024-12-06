using BrewBuddy.Interface;
using BrewBuddy.Models;

namespace BrewBuddy.Repositories
{
    public class StatistikRepository : IRepository<Statistik>
    {
        private readonly BrewBuddyContext _context;

        //vi laver en konstruktør som tager brewbuddycontext som parameter 
        public StatistikRepository(BrewBuddyContext context)
        {
            _context = context;
        }

        public void Add(Statistik entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Statistik> GetAll()
        {
            return _context.Statistiks.ToList();
        }

        public Statistik GetAllById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Statistik> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Statistik entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Statistik entity)
        {
            throw new NotImplementedException();
        }
    }
}
