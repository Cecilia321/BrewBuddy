using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BrewBuddy.Pages.Assignments
{
    public class AssignmentsModel : PageModel
    {
        private readonly IRepository<Assignment> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<Assignment> Assignments { get; set; }

        [BindProperty]
        public Assignment NewAssignment { get; set; }

        //derefter laver vi en konstruktør med repositori
        public AssignmentsModel(IRepository<Assignment> repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
            Assignments = _repository.GetAll();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Assignments = _repository.GetAll();
                return Page();
            }
            NewAssignment.FinishedDateAndTime = null;
            _repository.Add(NewAssignment);
            return RedirectToPage();
        }

        public IActionResult OnPostComplete(int AssignmentId)
        {
            //Debug.WriteLine("hallo");
            //Debug.WriteLine(AssignmentId);
            var assignment = _repository.GetAllById(AssignmentId);
            if (assignment == null) 
            {
                return Page();
            }
            assignment.IsComplete = true;
            assignment.FinishedDateAndTime = DateTime.Now;

            _repository.Update(assignment);

            Assignments = _repository.GetAll();

            return Page();

            
        }
    }
}
