using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BrewBuddy.Pages.Account
{
    public class LogInModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        public LogInModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        { 
            if (!ModelState.IsValid) return Page();

            //verificere email og password 
            if (Email == Email && Password == Password)
            {
                //laver security context - vi laver derfor en liste af claims 
                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Email, "Admin@mywebsite.com"),
                    new Claim("Role", "Admin") //til vores policy, ved at tilf�je dette claim kan vi nu bruge vore policy med samme "Role", "Admin"
                };
                
                var identity = new ClaimsIdentity(Claims, "MyCookieAuth"); //Vi tilf�jer nu listen af claims til en identity, vi bruger claimsidentity, s� vi kan tilf�je claims til og vores authenticationtype
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);//Nu har vi vores claimsprinsiple, s� nu skal vi bruge claimsprinsiple 

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");

            }
            return Page();
        }

    }
}
