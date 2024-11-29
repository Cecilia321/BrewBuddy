using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Security.Claims;

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
            Debug.WriteLine($"Assignments count: {Assignments?.Count ?? 0}");

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

        //public IActionResult OnPostComplete(int AssignmentId)
        //{
        //    //Debug.WriteLine("hallo");
        //    //Debug.WriteLine(AssignmentId);
        //    var assignment = _repository.GetAllById(AssignmentId);
        //    if (assignment == null)
        //    {
        //        return Page();
        //    }
        //    assignment.IsComplete = true;
        //    assignment.FinishedDateAndTime = DateTime.Now;
        //    assignment.UserId = ;


        //    _repository.Update(assignment);

        //    Assignments = _repository.GetAll();

        //    return Page();



        public IActionResult OnPostComplete(int AssignmentId)
        {
            // Hent opgaven fra databasen
            var assignment = _repository.GetAllById(AssignmentId);
            if (assignment == null)
            {
                ModelState.AddModelError("", "Opgaven findes ikke.");
                Assignments = _repository.GetAll();
                return Page();
            }

            // Hent UserId fra claims
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                ModelState.AddModelError("", "Du skal være logget ind for at fuldføre en opgave.");
                Assignments = _repository.GetAll();
                return Page();
            }

            // Marker opgaven som fuldført og tildel UserId
            assignment.IsComplete = true;
            assignment.FinishedDateAndTime = DateTime.Now;
            assignment.UserId = userId;

            // Opdater opgaven i databasen
            _repository.Update(assignment);

            // Opdater listen over opgaver
            Assignments = _repository.GetAll();

            return Page();
        }


    }
}

