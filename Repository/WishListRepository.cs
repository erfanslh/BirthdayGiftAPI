using BirthdayApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BirthdayApp.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly ApplicationDbContext _context;
        public WishListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WishList> AddAsync(WishList wishList)
        {
            await _context.Wishlists.AddAsync(wishList);
            return wishList;
        }

        public Task<WishList?> Remove(WishList wishList)
        {
            _context.Remove(wishList);
            return Task.FromResult<WishList?>(wishList);
        }

        public async Task<WishList?> GetByIdAsync(int id)
        {
            return await _context.Wishlists.FindAsync(id);
        }

        public async Task<IEnumerable<WishList>> GetUserWishListAsync(string userId)
        {
            return await _context.Wishlists.Where(x => x.OwnerId == userId).AsNoTracking().ToListAsync();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<WishList?> Update(WishList wishList)
        {
            _context.Wishlists.Update(wishList);
            return Task.FromResult(wishList)!;
        }

        public async Task<IEnumerable<WishList>> ViewFriendsWishList(string friendId)
        {
            return await _context.Wishlists.Where(x => x.OwnerId == friendId && x.IsBooked == false).AsNoTracking().ToListAsync();
        }
    }
}
