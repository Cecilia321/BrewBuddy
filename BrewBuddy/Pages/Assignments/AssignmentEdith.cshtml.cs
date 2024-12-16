using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Security.Claims;

namespace BrewBuddy.Pages.Assignments
{
    [Authorize(Policy = "AdminOnly")]
    public class AssignmentEdithModel : PageModel
    {
        private readonly IRepository<Assignment> _repository;
        private readonly IRepository<CoffieMachine> _machineRepository;


        //denne her laver vi for at holde maskinerne i en liste 
        public List<Assignment> Assignments { get; set; }

        public List<CoffieMachine> Machines { get; set; }


        [BindProperty]
        public Assignment NewAssignment { get; set; }

        //derefter laver vi en konstruktør med repositori
        public AssignmentEdithModel(IRepository<Assignment> repository, IRepository<CoffieMachine> machineRepository)
        {
            _repository = repository;
            _machineRepository = machineRepository;
        }


        public void OnGet()
        {
            // Get all assignments from the repository
            var allAssignments = _repository.GetAll();
            Assignments = allAssignments
            .GroupBy(a => a.Type)                // Gruppér efter opgavetype
            .Select(g => g.First())             // Tag den første opgave i hver gruppe
            .ToList();

            // Hent alle maskiner
            Machines = _machineRepository.GetAll();

        }


        public IActionResult OnPost()
        {
            Debug.WriteLine($"MachineId received: {NewAssignment.MachineId}");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("ModelState is invalid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine($"Error: {error.ErrorMessage}");
                }
                Assignments = _repository.GetAll();
                Machines = _machineRepository.GetAll(); // Hent maskiner igen, hvis der er fejl
                return Page();
            }

            // Hvis "Alle maskiner" er valgt (MachineId = 0)
            if (NewAssignment.MachineId == 0)
            {
                var allMachines = _machineRepository.GetAll();
                foreach (var machine in allMachines)
                {
                    var assignmentForMachine = new Assignment
                    {
                        Type = NewAssignment.Type,
                        Description = NewAssignment.Description,
                        IntervalType = NewAssignment.IntervalType,
                        MachineId = machine.MachineId, // Maskine ID for hver maskine
                        UserId = null,
                        FinishedDateAndTime = null,
                        Amount = NewAssignment.Amount,
                        IsComplete = false
                    };

                    _repository.Add(assignmentForMachine); // Tilføj opgaven for hver maskine
                }
            }
            else
            {
                // Hvis en specifik maskine er valgt, opret kun én opgave
                NewAssignment.UserId = null;
                NewAssignment.FinishedDateAndTime = null;
                _repository.Add(NewAssignment);
            }

            return RedirectToPage();
        }

        //public IActionResult OnPost()
        //{
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
        //    NewAssignment.UserId = null;
        //    NewAssignment.FinishedDateAndTime = null;
        //    _repository.Add(NewAssignment);
        //    return RedirectToPage();
        //}

        public IActionResult OnPostDelete(int AssignmentId)
        {
            _repository.Delete(AssignmentId);
            return RedirectToPage();
        }

    }
}
