using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BrewBuddy.Repositories
{
    //Her laver vi vores Machinerepository som implementere interfacet 
    public class CoffieMachineRepository : IRepository<CoffieMachine>
    {
        private readonly BrewBuddyContext _context;

        //vi laver en konstruktør som tager brewbuddycontext som parameter 
        public CoffieMachineRepository(BrewBuddyContext context)
        {
            _context = context;
        }

        public void Add(CoffieMachine coffieMachine)
        {
            _context.CoffieMachines.Add(coffieMachine); //her kan vi tilføje en ny maskine 
            _context.SaveChanges(); //denne er vigtig for det er den der sørger for at gemme den nye maskine 
        }

        public void Delete(int Id)
        {
            // Find the coffee machine by its ID
            var coffeeMachine = _context.CoffieMachines.Find(Id);

            // If the machine exists, remove it
            if (coffeeMachine != null)
            {
                _context.CoffieMachines.Remove(coffeeMachine);
                _context.SaveChanges(); // Save changes to commit the deletion
            }
        }

        public List<CoffieMachine> GetAll()
        {
            return _context.CoffieMachines.ToList(); //henter alle kaffemaskiner 
        }

        public Task<CoffieMachine> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        //public async Task<CoffieMachine> GetByIdAsync(int Id)
        //{
        //    return await _context.CoffieMachines.FirstOrDefaultAsync(m => m.MachineId == Id);
        //}

        public async Task UpdateAsync(CoffieMachine updateMachine)
        {
            _context.CoffieMachines.Update(updateMachine);
            await _context.SaveChangesAsync();
        }
    }
}
