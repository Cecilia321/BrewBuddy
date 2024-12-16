using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BrewBuddy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        public IndexModel(IRepository<User> userRepository)
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

            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == Email/* && u.Password == Password*/);

            //verificere email og password 
            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                //laver security context - vi laver derfor en liste af claims 
                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Email, user.Email),
                    //new Claim(ClaimTypes.NameIdentifier, user.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim("Role", user.Role), //til vores policy, ved at tilføje dette claim kan vi nu bruge vore policy med samme "Role", "Admin"
                    
                };

                var identity = new ClaimsIdentity(Claims, "MyCookieAuth"); //Vi tilføjer nu listen af claims til en identity, vi bruger claimsidentity, så vi kan tilføje claims til og vores authenticationtype
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);//Nu har vi vores claimsprinsiple, så nu skal vi bruge claimsprinsiple 

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Assignments/Assignments");

            }
            // Hvis bruger ikke findes eller password er forkert
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

    }
}

