using BrewBuddy.Interface;
using BrewBuddy.Models;

namespace BrewBuddy.Repositories
{
    public class AssignmentRepository : IRepository<Assignment>
    {
        private readonly BrewBuddyContext _context;

        //vi laver en konstruktør som tager brewbuddycontext som parameter 
        public AssignmentRepository(BrewBuddyContext context)
        {
            _context = context;
        }
        public void Add(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            _context.SaveChanges(); 
        }

        public void Delete(int Id)
        {
            // Find the coffee machine by its ID
            var assignment = _context.Assignments.Find(Id);

            // If the machine exists, remove it
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                _context.SaveChanges(); // Save changes to commit the deletion
            }
        }

        public List<Assignment> GetAll()
        {
            return _context.Assignments.ToList(); //henter alle kaffemaskiner 
        }

        public Task<Assignment> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Assignment assignment)
        {
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();
        }
    }
}
