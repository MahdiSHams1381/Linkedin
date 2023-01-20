using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using IMDB.DataLayer;
using Microsoft.EntityFrameworkCore;
using static Domain.ChatModels;

namespace Services
{
    public class ChatRepo
    {
        private readonly ContextDB _context;
        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
        }
        public async Task AddRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }
        public async Task<IEnumerable<Message>> GetMessagesAsync(Guid roomId)
        {
            return await _context.Messages.Where(n => n.RoomId == roomId).ToListAsync();
        }
        public async Task<IEnumerable<Room>> GetRoomsAsync(int userId)
        {
            return await _context.UserRooms.Where(n => n.UserId == userId).Include(n => n.Room).Select(n => n.Room).ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddUserToRoom(int userId , Guid RoomId)
        {
            await _context.UserRooms.AddAsync(new UserRoom()
            {
                RoomId = RoomId,
                UserId = userId,
                Id = Guid.NewGuid(),
            });
        }
    }
}