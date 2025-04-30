using BirthdayApp.Model;

namespace BirthdayApp.Repository
{
    public interface IWishListRepository
    {
        Task<IEnumerable<WishList>> GetUserWishListAsync(string userId);
        Task <WishList?> GetByIdAsync(int id);
        Task<WishList> AddAsync(WishList wishList);
        Task<WishList?> Remove(WishList wishList);
        Task<WishList?> Update(WishList wishList);
        Task SaveChangesAsync();
    }
}
