using BrewBuddy.Interface;
using BrewBuddy.Models;
using BrewBuddy.Pages.UserFolder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BrewBuddy.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly BrewBuddyContext _context; 

        public UserRepository(BrewBuddyContext context)
        {
            _context = context;
        }

        private void ValidateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName) || user.FirstName.Length < 1 || user.FirstName.Length > 50)
            {
                throw new UserValidationExeption("Fornavn skal være mellem 1 og 50 karakterer");
            }

            if (string.IsNullOrWhiteSpace(user.LastName) || user.LastName.Length < 1 || user.LastName.Length > 50)
            {
                throw new UserValidationExeption("Efternavn skal være mellem 1 og 50 karakterer");
            }
            //Validering af telefonnummer
            if (!Regex.IsMatch(user.PhoneNumber, @"^\d{8}$"))
            {
                throw new UserValidationExeption("Mobilnummer skal være præcis 8 cifre");
            }

            //validering af email.
            if (!Regex.IsMatch(user.Email, @"^.+@.+\..+$"))
            {
                throw new UserValidationExeption("Ugyldigt format");
            }

            //validering af password
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 0)
            {
                throw new UserValidationExeption("Password skal minimum være 8 karaktere");
            }

            if (user.BirthDate >= DateTime.Now)
            {
                throw new UserValidationExeption("Fødesldato skal være før dags dato.");
            }
            if (user.RegistrationDate == DateTime.MinValue)
            {
                user.RegistrationDate = DateTime.Now; // Eller en anden passende dato
            }
            user.RegistrationDate = user.RegistrationDate < new DateTime(1753, 1, 1)
            ? DateTime.Now
            : user.RegistrationDate;

        }

        public void Add(User user)
        {
            try
            {
                ValidateUser(user);
                
                    _context.Users.Add(user);
                    _context.SaveChanges();
                
            }
            catch (UserValidationExeption ex)
            {
                throw;
            }
        }

        public void Delete(int Id)
        {
            var user = _context.Users.Find(Id);

            // hvis brugeren eksistere, bliver det slettet 
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges(); 
            }
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetAllById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(int Id)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.UserId == Id);
        }

        public void Update(User entshitfuckity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(User updatedUser)
        {

            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();

        }
    }
}
