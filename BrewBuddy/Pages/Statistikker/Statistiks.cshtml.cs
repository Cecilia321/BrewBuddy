using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrewBuddy.Pages.Statistikker
{
    public class StatistiksModel : PageModel
    {
        //vi statrer med at injektisere repositoriet i coffiemachinmodel
        private readonly IRepository<Statistik> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<Statistik> Stat { get; set; }

        //og den her laver vi for at kunne oprette en ny maskine 
        [BindProperty]
        public Statistik NewStat { get; set; }

        //derefter laver vi en konstruktør med repositori
        public StatistiksModel(IRepository<Statistik> repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
            Stat = _repository.GetAll();

        }
    }
}
