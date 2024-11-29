using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BrewBuddy.Pages.MachineInfoMappe
{
    public class MachineInfosModel : PageModel
    {
        private readonly IRepository<MachineInfo> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<MachineInfo> Machines { get; set; }

        [BindProperty]
        public MachineInfo Info { get; set; }

        //derefter laver vi en konstruktør med repositori
        public MachineInfosModel(IRepository<MachineInfo> repository)
        {
            _repository = repository;
        }

        //    Debug.WriteLine($"MachineId received: {NewAssignment.MachineId}");

        //    if (!ModelState.IsValid)
        //    {
        //        Debug.WriteLine("ModelState is invalid");
        //        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        //        {
        //            Debug.WriteLine($"Error: {error.ErrorMessage}");
        //        }
        //        Assignments = _repository.GetAll();
        //        return Page();
        //    }

        //    NewAssignment.FinishedDateAndTime = null;
        //    _repository.Add(NewAssignment);
        //    return RedirectToPage();
        //}
        //public async Task<IActionResult> OnGet(int Id)
        //{
        //    if (Id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var machineInfo = await _repository.GetByIdAsync(Id);
        //    if (machineInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    Info = machineInfo;
        //    return Page();
        //}

        public void OnGet()
        {
            Machines = _repository.GetAll();

        }

    }
}
