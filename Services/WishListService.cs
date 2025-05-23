﻿using AutoMapper;
using BirthdayApp.DTO;
using BirthdayApp.Model;
using BirthdayApp.Repository;
using System.Data;

namespace BirthdayApp.Services
{
    public class WishListService : IWishListServices
    {
        private readonly IMapper _mapper;
        private readonly IWishListRepository _wishRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        public WishListService(IMapper mapper, IWishListRepository repository, IFriendshipRepository friendshipRepository)
        {
            _mapper = mapper;
            _wishRepository = repository;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<WishListDTO> AddAsync(string userid, AddWishListDTO addWishListDTO)
        {
            var addWish = _mapper.Map<WishList>(addWishListDTO);
            addWish.OwnerId = userid;
            addWish.IsBooked = false;

            await _wishRepository.AddAsync(addWish);
            await _wishRepository.SaveChangesAsync();

            return _mapper.Map<WishListDTO>(addWish);
        }

        #region Book_Unbook
        public async Task<WishListDTO> BookWishAsync(int id, string bookedByUser)
        {
            var bookWish = await _wishRepository.GetByIdAsync(id);
            if (bookWish == null || bookWish.IsBooked==true)
            {
                throw new Exception($"The Item that you requested {bookWish?.Title} is already booked by another person");
            }
            if (bookedByUser == bookWish.OwnerId)
            {
                throw new Exception($"You can not book your wish Item.");
            }

            var checkFriend = await _friendshipRepository.IsFriend(bookWish.OwnerId, bookedByUser);
            if (checkFriend != true)
            {
                throw new Exception($"You have no friendship with user {bookWish.OwnerId} please send a friend request to view his/her wishlist");
            }
            bookWish.IsBooked = true;
            bookWish.BookedByUser = bookedByUser;

            await _wishRepository.Update(bookWish);
            await _wishRepository.SaveChangesAsync();
            return _mapper.Map<WishListDTO>(bookWish);

        }
        public async Task<WishListDTO> UnBookWishAsync(int id, string bookedByUser)
        {
            var bookWish = await _wishRepository.GetByIdAsync(id);
            if (bookWish == null || bookWish.IsBooked == false)
            {
                throw new Exception($"The item with id: {id} is not booked or could not be found");
            }
            if (bookedByUser != bookWish.OwnerId)
            {
                throw new Exception("You have not booked this item");
            }

            bookWish.IsBooked = false;
            bookWish.BookedByUser = null;

            await _wishRepository.Update(bookWish);
            await _wishRepository.SaveChangesAsync();
            return _mapper.Map<WishListDTO>(bookWish);
        }
        #endregion

        public async Task<WishListDTO> DeleteAsync(int id, string userid)
        {
            var findWish = await _wishRepository.GetByIdAsync(id);
            if (findWish == null || findWish.OwnerId != userid)
            {
                throw new Exception($"There is no WishItem with ID: {id} for User: {userid}.");
            }
            await _wishRepository.Remove(findWish);
            await _wishRepository.SaveChangesAsync();

            return _mapper.Map<WishListDTO>(findWish);
        }

        public async Task<WishListDTO> GetByIdAsync(int id, string userid)
        {
            var findId = await _wishRepository.GetByIdAsync(id);
            if (findId == null || findId.OwnerId != userid)
            {
                throw new Exception($"A wishlist with id: {id} could not be found");
            }
            return _mapper.Map<WishListDTO>(findId);
        }

        public async Task<IEnumerable<WishListDTO>> GetMyWishListAsync(string userid)
        {
            var getUserList = await _wishRepository.GetUserWishListAsync(userid);
            return _mapper.Map<IEnumerable<WishListDTO>>(getUserList);
        }

        public async Task<WishListDTO> UpdateAsync(int id, string userid, UpdateWishListDTO updateDto)
        {
            var wishList = await _wishRepository.GetByIdAsync(id);
            if (wishList == null || wishList.OwnerId != userid)
            {
                throw new UnauthorizedAccessException("You can't update this item.");
            }

            await _wishRepository.Update(wishList);
            await _wishRepository.SaveChangesAsync();
            return _mapper.Map<WishListDTO>(updateDto);
        }

        public async Task<IEnumerable<WishListDTO>> ViewFriendsWishList(string friendId, string userid)
        {
            var checkFriendship = await _friendshipRepository.IsFriend(friendId, userid);
            if (checkFriendship != true)
            {
                throw new Exception($"You have no friendship with user {friendId} please send a friend request to view his/her wishlist");
            }
            var getFriendList = await _wishRepository.ViewFriendsWishList(friendId);
            if (getFriendList == null)
            {
                throw new Exception($"There is no wishlist for user {friendId}");
            }
            return _mapper.Map<IEnumerable<WishListDTO>>(getFriendList);
        }
    }
}
