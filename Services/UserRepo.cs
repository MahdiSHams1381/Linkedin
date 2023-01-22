using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using IMDB.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UserRepo
    {
        private ContextDB _context { get; }
        public UserRepo(ContextDB context)
        {
            _context = context;
        }

        public async Task AddSpecialityAsync(Specialty specialty)
        {
            if (!await _context.Specialties.AnyAsync(n => n.Title == specialty.Title))
                _context.Specialties.Add(specialty);
        }
        public async Task AddSpecialityToUserAsync(int userId, int SpecialtyId)
        {
            if (!await _context.SpecialtiesUsers.AnyAsync(n => n.SpecialtyId == SpecialtyId && n.UserId == userId))
                await _context.SpecialtiesUsers.AddAsync(new SpecialtyUser() { SpecialtyId = SpecialtyId, UserId = userId });
        }
        public async Task AddSpecialityToUserAsync(int userId, string SpecialtyTitle)
        {
            if (string.IsNullOrWhiteSpace(SpecialtyTitle))
                return;
            var specialty = await _context.Specialties.FirstOrDefaultAsync(n => n.Title == SpecialtyTitle);
            if (specialty == null)
            {
                specialty = new Specialty() { Title = SpecialtyTitle };
                await AddSpecialityAsync(specialty);
            }
            if ((specialty.Id == 0 || !await _context.SpecialtiesUsers.AnyAsync(n => n.SpecialtyId == specialty.Id && n.UserId == userId)))
            {
                await SaveChangesAsync();
                await _context.SpecialtiesUsers.AddAsync(new SpecialtyUser() { SpecialtyId = specialty.Id, UserId = userId });
            }
        }
        public async Task AddTwoWayConnectionAsync(int firstUser, int secondUser)
        {
            await _context.Connections.AddRangeAsync(new Connection[] { new Connection { FromUserId = firstUser, ToUserId = secondUser }, new Connection { FromUserId = secondUser, ToUserId = firstUser } });
        }
        public async Task AddOneWayConnectionAsync(int fromUser, int toUser)
        {
            await _context.Connections.AddAsync(new Connection { FromUserId = fromUser, ToUserId = toUser });
        }
        public async Task<User> GetUserAsync(int userId)
        {
            return await _context.Users.Include(n => n.UserSpecialties).ThenInclude(n => n.Specialty).FirstOrDefaultAsync(n => n.Id == userId);
        }
        public async Task<User> GetUserByNameOrId(string userId)
        {
            if (userId.All(n => Char.IsNumber(n) == true))
                return await GetUserAsync(int.Parse(userId));
            return await _context.Users.Include(n => n.UserSpecialties).ThenInclude(n => n.Specialty).FirstOrDefaultAsync(n => n.Name == userId);
        }

        public async Task AddUserAsync(User user)
        {
            if (!await _context.Users.AnyAsync(n => n.Name == user.Name))
                await _context.Users.AddAsync(user);
        }
        public async Task UpdateUserAsync(User user)
        {
            var curUser = await _context.Users.FindAsync(user.Id);
            curUser.WorkPlace = user.WorkPlace;
            curUser.DateOfBirth = user.DateOfBirth;
            curUser.Profile = user.Profile;
            curUser.Name = user.Name;
            curUser.Field = user.Field;
            curUser.UniversityLocation = user.UniversityLocation;
        }

        public async Task<bool> RemoveUserSpecialty(int userId, string specialtyTitle)
        {
            var spec = await _context.SpecialtiesUsers.Include(m => m.Specialty).FirstOrDefaultAsync(su => su.UserId == userId && su.Specialty.Title == specialtyTitle);
            if (spec == null)
                return false;
            _context.SpecialtiesUsers.Remove(spec);
            return true;
        }
        public async Task<bool> RemoveUserSpecialty(int userId, int specialtyId)
        {
            var spec = await _context.SpecialtiesUsers.FirstOrDefaultAsync(su => su.UserId == userId && su.SpecialtyId == specialtyId);
            if (spec == null)
                return false;
            _context.SpecialtiesUsers.Remove(spec);
            return true;
        }
        public async Task<bool> IsUserExistAsync(User user)
        {
            if (await _context.Users.AnyAsync(n => n.Name == user.Name))
                return true;
            return false;
        }
        public async Task<bool> IsUserExistAsync(string userName)
        {
            if (await _context.Users.AnyAsync(n => n.Name == userName))
                return true;
            return false;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public List<Specialty> GetSpecialties()
        {
            return _context.Specialties.ToList();
        }
        public IEnumerable<Specialty> GetSpecialties(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return null;
            List<Specialty> result = new List<Specialty>();
            foreach (var item in title.Split(' ', '-', '_', '/', '\'', '\"'))
            {
                if (!string.IsNullOrWhiteSpace(item))
                    result.AddRange(_context.Specialties.Where(n => n.Title.Contains(item)).ToList());
            }
            return result.Distinct();
        }
        public IEnumerable<User> GetAllUsersAsync()
        {
            var result =  _context.Users.Include(n=>n.UserSpecialties).ThenInclude(n=>n.Specialty).ToList();
            foreach (var user in result)
            {
                user.connection = _context.Connections.Where(n => n.FromUserId == user.Id).ToList();
            }
            return result;
        }
    }

}
