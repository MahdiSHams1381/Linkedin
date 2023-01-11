using System;
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
            var specialty = await _context.Specialties.FirstOrDefaultAsync(n => n.Title == SpecialtyTitle);
            if (specialty == null)
            {
                specialty = new Specialty() { Title = SpecialtyTitle };
                await AddSpecialityAsync(specialty);
            }
            if (specialty.Id == 0 || !await _context.SpecialtiesUsers.AnyAsync(n => n.SpecialtyId == specialty.Id && n.UserId == userId))
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
            return await _context.Users.Include(n=>n.UserSpecialties).ThenInclude(n=>n.Specialty).FirstOrDefaultAsync(n=>n.Id == userId);
        }
        public async Task<User> GetUserByNameOrId(string userId)
        {
            if(userId.Any(n => Char.IsDigit(n) == false))
                return await GetUserAsync(int.Parse(userId));
            return await _context.Users.Include(n => n.UserSpecialties).ThenInclude(n => n.Specialty).FirstOrDefaultAsync(n => n.Name == userId);
        }

        public async Task AddUserAsync(User user)
        {
            if (!await _context.Users.AnyAsync(n => n.Name == user.Name ))
                await _context.Users.AddAsync(user);
        }
        public async Task<bool> IsUserExistAsync(User user)
        {
            if (await _context.Users.AnyAsync(n => n.Name == user.Name))
                return true;
            return false;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
