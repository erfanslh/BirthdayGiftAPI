using BirthdayApp.DTO;

namespace BirthdayApp.Services
{
    public interface IFriendshipService
    {
        Task<FriendshipDTO> SendRequestAsync(string requesterId, string receiverId);
        Task<FriendshipDTO> UndoRequestAsync(string requesterId, string receiverId);
        Task<FriendshipDTO> GetFriendshipByIdAsync(int friendshipId);
        Task<bool> AcceptAsync(int friendshipId);
        Task<bool> DeclineAsync(int friendshipId);
        Task<IEnumerable<FriendshipDTO>> GetFriendAsync(string userId);
        Task<IEnumerable<FriendshipDTO>> GetPendingResultAsync(string userId);
        Task<FriendshipDTO> AcceptedFriendsAsync(string requesterId, string receiverId);
        Task<FriendshipDTO> PendingFriendRequestsAsync(string requesterId, string receiverId);
        Task<FriendshipDTO> Unfriend(string requesterId, string receiverId);
    }
}
