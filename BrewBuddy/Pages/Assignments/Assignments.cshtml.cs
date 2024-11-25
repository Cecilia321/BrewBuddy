using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrewBuddy.Pages.Assignments
{
    public class AssignmentsModel : PageModel
    {
        private readonly IRepository<Assignment> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<Assignment> Assignments { get; set; }

        //derefter laver vi en konstruktør med repositori
        public AssignmentsModel(IRepository<Assignment> repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
            Assignments = _repository.GetAll();

        }
    }
}
