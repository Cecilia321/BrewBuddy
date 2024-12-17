using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BrewBuddy.Pages.UserFolder
{
    [Authorize(Policy = "AdminOnly")]
    public class UsersModel : PageModel
    {
        //vi statrer med at injektisere repositoriet i coffiemachinmodel
        private readonly IRepository<User> _repository;


        //denne her laver vi for at holde maskinerne i en liste 
        public List<User> users { get; set; }


        //og den her laver vi for at kunne oprette en ny maskine 
        [BindProperty]
        public User NewUser { get; set; } 

        //Her genere vi en Bcrypt salt - ellers kunne vi skrive /*workFactor: 12*/ i stedet 
        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);

        //derefter laver vi en konstruktør med repositori
        public UsersModel(IRepository<User> repository) 
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
            try
            {
                NewUser.Password = BCrypt.Net.BCrypt.HashPassword(NewUser.Password, salt);
                _repository.Add(NewUser);
                return RedirectToPage();
            }
            catch (UserValidationExeption ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine($"DbUpdateException: {ex.InnerException?.Message}");
                throw;
            }
            catch (Exception ex)
            {
                users = _repository.GetAll();
                return Page();
            }
        }

        public IActionResult OnPostDelete(int UserId)
        {
            _repository.Delete(UserId);
            return RedirectToPage();
        }


    }
}
