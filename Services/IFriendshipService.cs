using BirthdayApp.DTO;

namespace BirthdayApp.Services
{
    public interface IFriendshipService
    {
        Task<FriendshipDTO> SendRequestAsync(string requesterId, string receiverId);
        Task<FriendshipDTO> UndoRequestAsync(string requesterId, string receiverId);
        Task<bool> BlockAsync(int friendshipId);
        Task<bool> UnblockAsync(int friendshipId);
        Task<bool> AcceptAsync(int friendshipId);
        Task<bool> DeclineAsync(int friendshipId);
        Task<IEnumerable<FriendshipDTO>> GetFriendAsync(string userId);
        Task<IEnumerable<FriendshipDTO>> GetPendingResultAsync(string userId);
    }
}
