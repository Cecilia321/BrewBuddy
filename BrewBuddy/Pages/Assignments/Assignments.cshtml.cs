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
    [Authorize]
    public class AssignmentsModel : PageModel
    {
        private readonly IRepository<Assignment> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<Assignment> Assignments { get; set; }
        public List<Assignment> IncompleteAssignments { get; set; }
        public List<Assignment> TodaysCompletedAssignments { get; set; }
        public List<Assignment> YesterdaysCompletedAssignments { get; set; }

        [BindProperty]
        public Assignment NewAssignment { get; set; }

        //derefter laver vi en konstruktør med repositori
        public AssignmentsModel(IRepository<Assignment> repository)
        {
            _repository = repository;
      
        }


        public void OnGet()
        {
            // Sørg for, at NewAssignment er initialiseret
            if (NewAssignment == null)
            {
                NewAssignment = new Assignment();
            }

            Debug.WriteLine("New Assignment added: " + NewAssignment.Type);

            // Hent alle opgaver fra repository
            var allAssignments = _repository.GetAll();
            PopulateAssignmentLists(allAssignments);
        }

            public IActionResult OnPostComplete(int AssignmentId, decimal? Amount)
            {
            // Check if Amount is valid
            //if (Amount < 0)
            //{
            //    ModelState.AddModelError("Amount", "Beløbet skal være større end 0.");
            //    IncompleteAssignments = GetIncompleteAssignments(_repository.GetAll()); // Refresh assignments list
            //    return Page(); // Return to the page with the error message
            //}
            // Hvis opgaven er af type 'bønner' eller 'mælkepulver', så skal Amount være gyldigt
           

            // Hent opgaven fra databasen
            var assignment = _repository.GetAllById(AssignmentId);
                if (assignment == null)
                {
                    ModelState.AddModelError("", "Opgaven findes ikke.");
                    IncompleteAssignments = GetIncompleteAssignments(_repository.GetAll());
                    return Page();
                }

                // Hent UserId fra claims
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    ModelState.AddModelError("", "Du skal være logget ind for at fuldføre en opgave.");
                    IncompleteAssignments = GetIncompleteAssignments(_repository.GetAll());
                    return Page();
                }

                if ((assignment.Type == "bønner" || assignment.Type == "mælkepulver") && Amount < 0)
            {
                ModelState.AddModelError("Amount", "Beløbet skal være større end 0.");
                IncompleteAssignments = GetIncompleteAssignments(_repository.GetAll()); // Refresh assignments list
                return Page(); // Return to the page with the error message
            }



            //Marker opgaven som fuldført og tildel UserId
            assignment.IsComplete = true;
                assignment.FinishedDateAndTime = DateTime.Now;
                assignment.UserId = userId;
                assignment.Amount = Amount;


                // Opdater opgaven i databasen
                _repository.Update(assignment);

                // Opdater listen over opgaver
                PopulateAssignmentLists(_repository.GetAll());

                return Page();
            }


            // Metode til at opdatere listerne
            private void PopulateAssignmentLists(IEnumerable<Assignment> allAssignments)
        {
            IncompleteAssignments = GetIncompleteAssignments(allAssignments);
            TodaysCompletedAssignments = GetTodaysCompletedAssignments(allAssignments);
            YesterdaysCompletedAssignments = GetYesterdaysCompletedAssignments(allAssignments);
        }

        // Metoder til filtrering
        private List<Assignment> GetIncompleteAssignments(IEnumerable<Assignment> allAssignments)
        {
            return allAssignments
                .Where(a => !a.IsComplete)
                .ToList();
        }

        private List<Assignment> GetTodaysCompletedAssignments(IEnumerable<Assignment> allAssignments)
        {
            var today = DateTime.Today;
            return allAssignments
                 // Sørg for at inkludere User
                .Where(a => a.IsComplete && a.FinishedDateAndTime?.Date == today)
     
                .OrderBy(a => a.FinishedDateAndTime)
                
                .ToList();
                
        }

        private List<Assignment> GetYesterdaysCompletedAssignments(IEnumerable<Assignment> allAssignments)
        {
            var yesterday = DateTime.Today.AddDays(-1);
            return allAssignments
                .Where(a => a.IsComplete && a.FinishedDateAndTime?.Date == yesterday)
                .OrderBy(a => a.FinishedDateAndTime)
                .ToList();
        }

    }
}

