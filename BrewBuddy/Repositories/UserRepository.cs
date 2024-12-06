using BrewBuddy.Interface;
using BrewBuddy.Models;

namespace BrewBuddy.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly BrewBuddyContext _context; 

        public UserRepository(BrewBuddyContext context)
        {
            _context = context;
        }



        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var user = _context.Users.Find(Id);

            // hvis brugeren eksistere, bliver det slettet 
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges(); 
            }
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetAllById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
