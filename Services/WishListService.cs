using AutoMapper;
using BirthdayApp.DTO;
using BirthdayApp.Model;
using BirthdayApp.Repository;

namespace BirthdayApp.Services
{
    public class WishListService : IWishListServices
    {
        private readonly IMapper _mapper;
        private readonly IWishListRepository _repository;
        public WishListService(IMapper mapper, IWishListRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<WishListDTO> AddAsync(string userid, AddWishListDTO addWishListDTO)
        {
            var addWish = _mapper.Map<WishList>(addWishListDTO);
            addWish.OwnerId = userid;

            await _repository.AddAsync(addWish);
            await _repository.SaveChangesAsync();

            return _mapper.Map<WishListDTO>(addWish);
        }

        public async Task<WishListDTO> BookWishAsync(int id, string bookedByUser)
        {
            var bookWish = await _repository.GetByIdAsync(id);
            if (bookWish == null || bookWish.IsBooked==true)
            {
                throw new Exception($"The Item that you requested {bookWish?.Title} is already booked by another person");
            }

            bookWish.IsBooked = true;
            bookWish.BookedByUser = bookedByUser;

            await _repository.Update(bookWish);
            await _repository.SaveChangesAsync();
            return _mapper.Map<WishListDTO>(bookWish);

        }

        public async Task<WishListDTO> DeleteAsync(int id, string userid)
        {
            var findWish = await _repository.GetByIdAsync(id);
            if (findWish == null || findWish.OwnerId != userid)
            {
                throw new Exception($"There is no WishItem with ID: {id} for User: {userid}.");
            }
            await _repository.Remove(findWish);
            await _repository.SaveChangesAsync();

            return _mapper.Map<WishListDTO>(findWish);
        }

        public async Task<WishListDTO> GetByIdAsync(int id)
        {
            var findId = await _repository.GetByIdAsync(id);
            if (findId == null)
            {
                throw new Exception($"A wishlist with id: {id} could not be found");
            }
            return _mapper.Map<WishListDTO>(findId);
        }

        public async Task<IEnumerable<WishListDTO>> GetUserWishListAsync(string userid)
        {
            var getUserList = await _repository.GetUserWishListAsync(userid);
            if (getUserList == null)
            {
                throw new Exception($"there is no WishList for the {userid}");
            }
            return _mapper.Map<IEnumerable<WishListDTO>>(getUserList);
        }

        public async Task<WishListDTO> UpdateAsync(int id, string userid, UpdateWishListDTO updateDto)
        {
            var wishList = await _repository.GetByIdAsync(id);
            if (wishList == null || wishList.OwnerId != userid)
            {
                throw new UnauthorizedAccessException("You can't update this item.");
            }

            await _repository.Update(wishList);
            await _repository.SaveChangesAsync();
            return _mapper.Map<WishListDTO>(wishList);
        }
    }
}
