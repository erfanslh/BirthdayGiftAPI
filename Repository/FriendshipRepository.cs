using BirthdayApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BirthdayApp.Repository
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly ApplicationDbContext _context;
        public FriendshipRepository(ApplicationDbContext dbContext)
        {
                _context = dbContext;
        }
        public async Task<Friendship> AddAsync(Friendship friendship)
        {
            await _context.Friendships.AddAsync(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }

        public async Task<Friendship?> GetByIdAsync(int id)
        {
            var findFrienship = await _context.Friendships.FindAsync(id);
            if (findFrienship is null)
            {
                throw new Exception($"Friendship with id: {id} could not be found found");
            }
            return findFrienship;
        }

        public async Task<Friendship?> GetFriendshipAsync(string requesterId, string receiverId)
        {
            var findFriendship = await _context.Friendships.FirstOrDefaultAsync(x => x.ReceiverId == receiverId && x.RequesterId == requesterId);
            if (findFriendship is null)
            {
                throw new Exception($"There is no Friendship between User with {requesterId} and User with {receiverId} ID ");
            }
            return findFriendship;
        }

        public async Task<IEnumerable<Friendship>> GetFriendsOfUserAsync(string userId)
        {
            return await _context.Friendships.AsNoTracking()
                .Where(x => (x.RequesterId == userId || x.ReceiverId == userId) && (x.Status == FriendshipStatus.Accepted))
                .ToListAsync();
        }

        public async Task<IEnumerable<Friendship>> GetPendingRequestsAsync(string userId)
        {
            return await _context.Friendships.AsNoTracking()
                .Where(x => (x.RequesterId == userId || x.ReceiverId == userId) && (x.Status == FriendshipStatus.Pending))
                .ToListAsync();
        }

        public async Task<Friendship?> Remove(int id)
        {
            var findFriendship = await GetByIdAsync(id);
            if (findFriendship is null)
            {
                throw new Exception($"There is no Friendship with the person id: {id} to remove");
            }

            _context.Friendships.Remove(findFriendship);
            await _context.SaveChangesAsync();
            return findFriendship;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
