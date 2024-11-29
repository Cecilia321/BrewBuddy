using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrewBuddy.Pages.MachineInfoMappe
{
    public class MachineInfosModel : PageModel
    {
        private readonly IRepository<MachineInfo> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<CoffieMachine> coffieMachines { get; set; }

        //derefter laver vi en konstruktør med repositori
        public MachineInfosModel(IRepository<MachineInfo> repository)
        {
            _repository = repository;
        }
        public void OnGet()
        {
        }
    }
}
