using BirthdayApp.Model;
using BirthdayApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace BirthdayApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        
        private readonly IFriendshipService _service;
        public FriendshipController(IFriendshipService service)
        {
            _service = service;
        }


        [HttpPost("request")]
        public async Task<IActionResult> SendRequest(string receiverID)
        {
            var requesterID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (requesterID == null)
            {
                return Unauthorized();
            }
            var result = await _service.SendRequestAsync(requesterID, receiverID);
            return Ok(result);
        }

        [HttpPost("accept/{id}")]
        public async Task<IActionResult> AcceptRequest(int id)
        {
            var findUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (findUser == null)
            {
                return Unauthorized();
            }
            var getFriendshipID = await _service.GetFriendshipByIdAsync(id);
            if (getFriendshipID.RequesterId == findUser)
            {
                return BadRequest("Request has to be answered by the receiver");
            }

            var success = await _service.AcceptAsync(id);
            
            if (!success)
            {
                return BadRequest("The request cannot be accepted");
            }
            return Ok("Friend request has been accepted");
        }

        [HttpPost("reject/{id}")]
        public async Task<IActionResult> DeclineRequest(int id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user is null)
            {
                return Unauthorized();
            }
            var friendshipIp = await _service.GetFriendshipByIdAsync(id);
            if (user == friendshipIp.RequesterId )
            {
                return BadRequest("Request has to be answered by the receiver");
            }
            var reject = await _service.DeclineAsync(id);
            if (!reject)
            {
                return BadRequest("The request cannot be declined");
            }
            return Ok("The request has been rejected");
        }

        [HttpDelete("Unfriend")]
        public async Task<IActionResult> Unfriend(string receiverId)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (requesterId == null)
            {
                return Unauthorized();
            }
            var finalFriendship = await _service.AcceptedFriendsAsync(requesterId, receiverId);
            if (finalFriendship.RequesterId != requesterId)
            {
                return Unauthorized();
            }
            var final = await _service.Unfriend(requesterId, receiverId);
            return Ok(final);
        }
        [HttpDelete("undo")]
        public async Task<IActionResult> UndoRequest(string receiverId)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (requesterId == null)
            {
                return Unauthorized();
            }
            var getFriendshipID = await _service.PendingFriendRequestsAsync(requesterId, receiverId);
            if (getFriendshipID.RequesterId != requesterId)
            {
                return Unauthorized();
            }
            var resutlt = await _service.UndoRequestAsync(requesterId,receiverId);
            return Ok(resutlt);
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetAllFriends()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user is null)
            {
                return Unauthorized();
            }

            var friendList = await _service.GetFriendAsync(user);
            return Ok(friendList);
        }
        
        [HttpGet("pendingrequests")]
        public async Task<IActionResult> GetPendingRequests()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user is null)
            {
                return Unauthorized();
            }

            var getPendings = await _service.GetPendingResultAsync(user);
            return Ok(getPendings);
        }
    }
}
