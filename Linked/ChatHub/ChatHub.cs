using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Linked.Controllers;
using Microsoft.AspNetCore.SignalR;
using Services;
using static Domain.ChatModels;

namespace Linked.ChatHub
{
    public class ChatHub:Hub
    {
        private readonly ChatRepo _chatRepo;
        private readonly UserRepo _userRepo;
        public ChatHub(ChatRepo chatRepo, UserRepo userRepo)
        {
            _chatRepo = chatRepo;
            _userRepo = userRepo;
        }

        public override async Task OnConnectedAsync()
        {
            var utility = new Utility();
            var userId = await utility.GetAuthenticatedUser();
            await _userRepo.GetUserAsync(userId);
            var rooms = await _chatRepo.GetRoomsAsync(userId);
            foreach (var room in rooms)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
            }
            await base.OnConnectedAsync();
        }
        public async Task AddToRoom(int OwnerId,int SecondUserId)
        {
            var roomId = Guid.NewGuid();
            await _chatRepo.AddRoom(new Domain.ChatModels.Room()
            {
                Id = roomId,
                Name = $"Chat Beetwin {(await _userRepo.GetUserAsync(OwnerId)).Name} & {(await _userRepo.GetUserAsync(SecondUserId)).Name}",
                OwnerId = OwnerId,
            });
            await _chatRepo.AddUserToRoom(OwnerId , roomId);
            await _chatRepo.AddUserToRoom(SecondUserId, roomId);
            await _chatRepo.SaveChangesAsync();
        }
        public async Task SendMessage(string content,int SenderId,Guid RoomId)
        {
            var message = new Domain.ChatModels.Message()
            {
                Content = content,
                SenderId = SenderId,
                RoomId = RoomId,
                SendTime = DateTime.Now,
            };
            await _chatRepo.AddMessage(message);
            await Clients.Group(RoomId.ToString()).SendAsync("Receive" , message);
        }
        public async Task JoinGroup(Guid roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }
        public async Task LeaveGroup(Guid roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }

    }
}
