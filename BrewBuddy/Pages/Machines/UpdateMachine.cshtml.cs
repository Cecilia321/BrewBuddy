using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrewBuddy.Pages.Machines
{
    public class UpdateMachineModel : PageModel
    {
        //vi statrer med at injektisere repositoriet i coffiemachinmodel
        private readonly IRepository<CoffieMachine> _repository;
        public UpdateMachineModel(IRepository<CoffieMachine> repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public CoffieMachine UpdateMachine { get; set; }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }

            var machine = await _repository.GetByIdAsync(Id);
            if (machine == null)
            {
                return NotFound();
            }

            UpdateMachine = machine;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _repository.UpdateAsync(UpdateMachine);

            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _repository.GetByIdAsync(UpdateMachine.MachineId);
                if (exists == null)
                {
                    return NotFound();

                }
                else
                {
                    throw;
                }


            }
            return RedirectToPage("/Machines/EditCoffieMachines");
        }
    }
}
