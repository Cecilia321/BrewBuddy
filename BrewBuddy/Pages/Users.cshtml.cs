using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrewBuddy.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class UsersModel : PageModel
    {
        private readonly IRepository<User> _repository; //vi statrer med at injektisere repositoriet i coffiemachinmodel

        public List<User> users { get; set; } //denne her laver vi for at holde maskinerne i en liste 

        
        [BindProperty]
        public User NewUser { get; set; } //og den her laver vi for at kunne oprette en ny maskine 

        
        public UsersModel(IRepository<User> repository) //derefter laver vi en konstruktør med repositori
        {
            _repository = repository;
        }


        public void OnGet()
        {
            users = _repository.GetAll();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                users = _repository.GetAll();
                return Page();
            }
            _repository.Add(NewUser);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int UserId)
        {
            _repository.Delete(UserId);
            return RedirectToPage();
        }
    }
}
