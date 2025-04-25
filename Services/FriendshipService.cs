using AutoMapper;
using BirthdayApp.DTO;
using BirthdayApp.Model;
using BirthdayApp.Repository;

namespace BirthdayApp.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _repository;
        private readonly IMapper _mapper;
        public FriendshipService(IFriendshipRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task<bool> BlockAsync(int friendshipId)
        {
            var checkFriendship = await _repository.GetByIdAsync(friendshipId);
            if (checkFriendship is null)
            {
                throw new Exception("The friendship that u have requested is not found");
            }
            if (checkFriendship.Status == FriendshipStatus.Blocked)
            {
                throw new Exception("The user has been Blocked");
            }

            checkFriendship.Status = FriendshipStatus.Blocked;
            await _repository.SaveChangesAsync();
            return true;
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
            var checkFriendship = await _repository.GetFriendshipAsync(requesterId, receiverId);
            if (checkFriendship is not null)
            {
                throw new Exception($"A Friendship between User with id: {requesterId} and User with id: {receiverId} exists.");
            }

            var newFriendship = new Friendship
            {
                ReceiverId = receiverId,
                RequesterId = requesterId,
                Status = FriendshipStatus.Pending
            };

            await _repository.AddAsync(newFriendship);
            return _mapper.Map<FriendshipDTO>(newFriendship);
        }

        public async Task<bool> UnblockAsync(int friendshipId)
        {
            var checkFriendship = await _repository.GetByIdAsync(friendshipId);
            if (checkFriendship is null)
            {
                throw new Exception("The friendship that u have requested is not found");
            }
            if (checkFriendship.Status != FriendshipStatus.Blocked)
            {
                throw new Exception("The user is not blocked");
            }
            checkFriendship.Status = FriendshipStatus.Rejected;
            await _repository.SaveChangesAsync();
            return true;
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
    }
}
