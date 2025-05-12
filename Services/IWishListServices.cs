using BirthdayApp.DTO;

namespace BirthdayApp.Services
{
    public interface IWishListServices
    {
        Task<IEnumerable<WishListDTO>> GetMyWishListAsync(string userid);
        Task<IEnumerable<WishListDTO>> ViewFriendsWishList(string friendId, string userid);
        Task<WishListDTO> GetByIdAsync(int id, string userid);
        Task<WishListDTO> UpdateAsync(int id, string userid, UpdateWishListDTO updateDto);
        Task<WishListDTO> DeleteAsync(int id, string userid);
        Task<WishListDTO> AddAsync(string userid, AddWishListDTO addWishListDTO);
        Task<WishListDTO> BookWishAsync(int id, string bookedByUser);
        Task<WishListDTO> UnBookWishAsync(int id, string bookedByUser);
        

    }
}
