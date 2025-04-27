using AutoMapper;
using BirthdayApp.DTO;
using BirthdayApp.Model;
using BirthdayApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace BirthdayApp.Services
{
    public class FriendshipService : IFriendshipService
        
    {
        private readonly IFriendshipRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public FriendshipService(IFriendshipRepository repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> AcceptAsync(int friendshipId)
        {
            var checkFriendship = await _repository.GetByIdAsync(friendshipId);
            if (checkFriendship is null)
            {
                throw new Exception("The friendship that u have requested is not found");
            }
            if (checkFriendship.Status != FriendshipStatus.Pending)
            {
                throw new Exception("Only pending Requests can be accepted");
            }
            if (checkFriendship.Status == FriendshipStatus.Accepted)
            {
                throw new Exception("The request has been already Accepted");
            }
            checkFriendship.Status = FriendshipStatus.Accepted;
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<FriendshipDTO> AcceptedFriendsAsync(string requesterId, string receiverId)
        {
            var final = await _repository.AcceptedFriendshipAsync(requesterId, receiverId);
            if (final is null)
            {
                throw new Exception("You have no friends yet");
            }
            return _mapper.Map<FriendshipDTO>(final);
        }
        public async Task<FriendshipDTO> PendingFriendRequestsAsync(string requesterId, string receiverId)
        {
            var final = await _repository.PendingRequestAsync(requesterId, receiverId);
            if (final is null)
            {
                throw new Exception("You have not any friend Request received");
            }
            return _mapper.Map<FriendshipDTO>(final);
        }
        public async Task<bool> DeclineAsync(int friendshipId)
        {
            var checkFriendship = await _repository.GetByIdAsync(friendshipId);
            if (checkFriendship is null)
            {
                throw new Exception("The friendship that u have requested is not found");
            }
            if (checkFriendship.Status != FriendshipStatus.Pending)
            {
                throw new Exception("Only pending Requests can be rejected");
            }
            checkFriendship.Status = FriendshipStatus.Rejected;
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FriendshipDTO>> GetFriendAsync(string userId)
        {
            var findFriendship = await _repository.GetFriendsOfUserAsync(userId);
            if (findFriendship is null)
            {
                throw new Exception($"User with id: {userId} could not found");
            }
            return _mapper.Map<IEnumerable<FriendshipDTO>>(findFriendship);
             
        }

        public async Task<FriendshipDTO> GetFriendshipByIdAsync(int friendshipId)
        {
            var friendship = await _repository.GetByIdAsync(friendshipId);
            return _mapper.Map<FriendshipDTO>(friendship);
        }

        public async Task<IEnumerable<FriendshipDTO>> GetPendingResultAsync(string userId)
        {
            var findFriendship = await _repository.GetPendingRequestsAsync(userId);
            if (findFriendship is null)
            {
                throw new Exception($"User with id: {userId} could not found");
            }
            return _mapper.Map<IEnumerable<FriendshipDTO>>(findFriendship);
        }

        public async Task<FriendshipDTO> SendRequestAsync(string requesterId, string receiverId)
        {
            var requesterName = await _userManager.FindByIdAsync(requesterId);
            var receiverName = await _userManager.FindByIdAsync(receiverId);

            var newFriendship = new Friendship
            {
                ReceiverId = receiverId,
                RequesterId = requesterId,
                Status = FriendshipStatus.Pending
            };

            await _repository.AddAsync(newFriendship);
            return _mapper.Map<FriendshipDTO>(newFriendship);
        }

        public async Task<FriendshipDTO> UndoRequestAsync(string requesterId, string receiverId)
        {
            var checkFriendship = await _repository.GetFriendshipAsync(requesterId, receiverId);
            if (checkFriendship is null)
            {
                throw new Exception($"there is no Friendship between user id: {requesterId} and {receiverId} to undo Request");
            }
            if (checkFriendship.Status != FriendshipStatus.Pending)
            {
                throw new Exception("Only pending Requests can be undo...");
            }

            var request = await _repository.GetFriendshipAsync(requesterId, receiverId);
            if (request is null)
            {
                throw new Exception("Only pending Requests can be undone...");
            }
            if (request.Status != FriendshipStatus.Pending)
            {
                throw new Exception("Only pending Requests can be undone...");
            }

            await _repository.Remove(checkFriendship.Id);
            var mapper = _mapper.Map<FriendshipDTO>(checkFriendship);
            await _repository.SaveChangesAsync();
            return mapper;
        }

        public async Task<FriendshipDTO> Unfriend(string requesterId,string receiverId)
        {
            var Findfriendship = await _repository.AcceptedFriendshipAsync(requesterId, receiverId);
            if (Findfriendship is null)
            {
                throw new Exception($"You have no Friendship with {receiverId}");
            }
            await _repository.Remove(Findfriendship.Id);
            return _mapper.Map<FriendshipDTO>(Findfriendship);

        }
    }
}
