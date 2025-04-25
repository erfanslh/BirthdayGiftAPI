using BirthdayApp.Model;

namespace BirthdayApp.Repository
{
    public interface IFriendshipRepository
    {
        // add a friend request
        Task <Friendship> AddAsync(Friendship friendship);
        Task <Friendship?> Remove(int id);
        // get a friendship by requester and receiver id
        Task<Friendship?> GetFriendshipAsync(string requesterId, string receiverId);
        // get all friends of a user
        Task<IEnumerable<Friendship>> GetFriendsOfUserAsync(string userId);
        // get all pending requests of a user
        Task<IEnumerable<Friendship>> GetPendingRequestsAsync(string userId);
        // get a friendship by id
        Task<Friendship?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
