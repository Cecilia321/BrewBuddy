using BrewBuddy.Interface;
using BrewBuddy.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

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
        public Assignment NewAssignmenten { get; set; }

        //derefter laver vi en konstruktør med repositori
        public AssignmentEdithModel(IRepository<Assignment> repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
            // Get all assignments from the repository
            Assignments = _repository.GetAll();
            
        }

        public IActionResult OnPost()
        {
            Debug.WriteLine($"MachineId received: {NewAssignmenten.MachineId}");

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
            NewAssignmenten.UserId = null;
            NewAssignmenten.FinishedDateAndTime = null;
            _repository.Add(NewAssignmenten);
            return RedirectToPage();
        }
    }
}
