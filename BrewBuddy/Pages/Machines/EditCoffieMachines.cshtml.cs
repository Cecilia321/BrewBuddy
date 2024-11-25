using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BrewBuddy.Pages.Machines
{
//    [Authorize(Policy = "AdminOnly")]
    public class EditCoffieMachinesModel : PageModel
    {
        //vi statrer med at injektisere repositoriet i coffiemachinmodel
        private readonly IRepository<CoffieMachine> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<CoffieMachine> coffieMachines { get; set; }

        //og den her laver vi for at kunne oprette en ny maskine 
        [BindProperty]
        public CoffieMachine NewMachine { get; set; }

        //derefter laver vi en konstruktør med repositori
        public EditCoffieMachinesModel(IRepository<CoffieMachine> repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
            coffieMachines = _repository.GetAll();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                coffieMachines = _repository.GetAll();
                return Page();
            }
            _repository.Add(NewMachine);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int MachineId)
        {
            _repository.Delete(MachineId);
            return RedirectToPage();
        }

        //public IActionResult OnPostUpdate()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        coffieMachines = _repository.GetAll();
        //        return Page();
        //    }
        //    _repository.Update(UpdatedMachine);
        //    return RedirectToPage();
        //}

        //public IActionResult OnPostSelectForUpdate(int MachineId)
        //{
        //    var machine = _repository.GetAll().FirstOrDefault(m => m.MachineId == MachineId);

        //    if (machine != null)
        //    {
        //        UpdatedMachine = machine;
        //    }

        //    coffieMachines = _repository.GetAll();
        //    return Page();
        //}
    }
}


