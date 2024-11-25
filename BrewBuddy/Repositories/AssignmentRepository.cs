using BrewBuddy.Interface;
using BrewBuddy.Models;

namespace BrewBuddy.Repositories
{
    public class AssignmentRepository : IRepository<Assignment>
    {
        public void Add(Assignment assignment)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Assignment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Assignment> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Assignment entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Assignment entity)
        {
            throw new NotImplementedException();
        }
    }
}
