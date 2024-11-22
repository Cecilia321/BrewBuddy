using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace BrewBuddy.Pages
{
    public class CoffieMachinesModel : PageModel
    {
        //vi statrer med at injektisere repositoriet i coffiemachinmodel
        private readonly IRepository<CoffieMachine> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<CoffieMachine> coffieMachines { get; set; }

   

        //derefter laver vi en konstruktør med repositori
        public CoffieMachinesModel(IRepository<CoffieMachine> repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
            coffieMachines = _repository.GetAll();

        }
        
    }
}
