using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Security.Claims;

namespace BrewBuddy.Pages.Assignments
{
    public class AssignmentEdithModel : PageModel
    {
        private readonly IRepository<Assignment> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<Assignment> Assignments { get; set; }
        //public List<Assignment> IncompleteAssignments { get; set; }
        //public List<Assignment> TodaysCompletedAssignments { get; set; }
        //public List<Assignment> YesterdaysCompletedAssignments { get; set; }

        [BindProperty]
        public Assignment NewAssignment { get; set; }

        //derefter laver vi en konstruktør med repositori
        public AssignmentEdithModel(IRepository<Assignment> repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
            // Get all assignments from the repository
            var allAssignments = _repository.GetAll();
            Assignments = allAssignments
            .GroupBy(a => a.Type)                // Gruppér efter opgavetype
            .Select(g => g.First())             // Tag den første opgave i hver gruppe
            .ToList();

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
                return Page();
            }
            NewAssignment.UserId = null;
            NewAssignment.FinishedDateAndTime = null;
            _repository.Add(NewAssignment);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int AssignmentId)
        {
            _repository.Delete(AssignmentId);
            return RedirectToPage();
        }

    }
}
