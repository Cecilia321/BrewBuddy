using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrewBuddy.Pages.UserFolder
{
    public class UpdateUserModel : PageModel
    {
            //vi statrer med at injektisere repositoriet i coffiemachinmodel
            private readonly IRepository<User> _repository;
            public UpdateUserModel(IRepository<User> repository)
            {
                _repository = repository;
            }

            [BindProperty]
            public User UpdateUser { get; set; }

            public async Task<IActionResult> OnGetAsync(int Id)
            {
                if (Id == 0)
                {
                    return NotFound();
                }

                var user = await _repository.GetByIdAsync(Id);
                if (user == null)
                {
                    return NotFound();
                }

                UpdateUser = user;
                return Page();
            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                try
                {
                    await _repository.UpdateAsync(UpdateUser);

                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await _repository.GetByIdAsync(UpdateUser.UserId);
                    if (exists == null)
                    {
                        return NotFound();

                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToPage("/UserFolder/Users");
            }
        }
    }

